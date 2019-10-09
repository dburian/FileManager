using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Search operation execution context.
	/// </summary>
	internal class SearchWrapper : ISearchView
	{
		private const int MIN_FOUND_BATCH_LENGTH = 1;
		private const int MAX_FOUND_BATCH_LENGTH = 30 * MIN_FOUND_BATCH_LENGTH;
		private readonly FileSystemNodeSearch searchEngine;
		private readonly CancellationTokenSource cts;
		private bool disposed;
		private readonly BlockingCollection<FileSystemInfo> foundBuffer;
		private readonly Consumer consumer;

		public SearchWrapper(FileSystemNodeSearch searchEngine, CancellationTokenSource cts, Task task)
		{
			this.searchEngine = searchEngine;
			this.cts = cts;
			ProducerTask = task;

			foundBuffer = new BlockingCollection<FileSystemInfo>(MAX_FOUND_BATCH_LENGTH * 2);

			searchEngine.ExceptionRise += OnExceptionRaise;
			searchEngine.NodeFound += OnNodeFound;
			ProducerTask.ContinueWith(_ => OnSearchDone(), new CancellationToken(), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);

			consumer = new Consumer(this);
		}

		public SearchSettings Settings => searchEngine.Settings;
		public Task ProducerTask { get; }

		public event OnSearchDoneDelegate SearchDone;
		public event OnExceptionRaiseDelegate ExceptionRaise;
		public event OnFoundBatchFullDelegate FoundBatchFull
		{
			add => consumer.FoundBatchFull += value;
			remove => consumer.FoundBatchFull -= value;
		}

		/// <summary>
		/// Cancels the search.
		/// </summary>
		public void Cancel()
		{
			cts.Cancel();
			consumer.Stop();
		}

		/// <summary>
		/// Starts the search.
		/// </summary>
		public void Start()
		{
			ProducerTask.Start(TaskScheduler.Default);
		}

		public void Dispose()
		{
			disposed = true;

			searchEngine.ExceptionRise -= OnExceptionRaise;
			searchEngine.NodeFound -= OnNodeFound;

			cts.Cancel();
			consumer.Dispose();
			foundBuffer.Dispose();
		}

		private void OnNodeFound(FileSystemInfo nodeFound)
		{
			if (disposed)
			{
				return;
			}

			if (foundBuffer.Count > MIN_FOUND_BATCH_LENGTH && Monitor.TryEnter(consumer.emptyingBuffer))
			{
				Monitor.Pulse(consumer.emptyingBuffer);
				Monitor.Exit(consumer.emptyingBuffer);
			}

			foundBuffer.Add(nodeFound);
		}

		private void OnExceptionRaise(FileOperationException e)
		{
			if (disposed)
			{
				return;
			}

			ExceptionRaise?.Invoke(e);
		}

		private void OnSearchDone()
		{
			if (disposed)
			{
				return;
			}

			consumer.Flush();

			SearchDone?.Invoke();
		}

		/// <summary>
		/// Execution context of the consumer thread.
		/// </summary>
		private class Consumer : IDisposable
		{
			private const int MIN_PAUSE_BETWEEN_BATCHES = 20;
			private const int MAX_PAUSE_BETWEEN_BATCHES = 800;

			public object emptyingBuffer;
			private bool poisonPill;
			private int infosSent = 0;
			private DateTime lastBatchTime;
			private readonly Thread cleaningThread;
			private readonly SearchWrapper producer;

			public Consumer(SearchWrapper producer)
			{
				emptyingBuffer = new object();

				this.producer = producer;

				lastBatchTime = DateTime.Now;
				cleaningThread = new Thread(Consume);
				cleaningThread.Start();
			}

			public event OnFoundBatchFullDelegate FoundBatchFull;

			public void Dispose()
			{
				Stop();

				cleaningThread.Join();
			}

			/// <summary>
			/// Stops the consumer thread.
			/// </summary>
			public void Stop()
			{
				if (!cleaningThread.IsAlive)
				{
					return;
				}

				poisonPill = true;

				lock (emptyingBuffer)
				{
					Monitor.Pulse(emptyingBuffer);
				}
			}

			/// <summary>
			/// Consumes all the findings.
			/// </summary>
			public void Flush()
			{
				lock (emptyingBuffer)
				{
					while (producer.foundBuffer.Count > 0 && !poisonPill)
					{
						ShipBatch();
					}
				}
			}

			/// <summary>
			/// Main consumer loop.
			/// </summary>
			public void Consume()
			{
				lock (emptyingBuffer)
				{
					while (!poisonPill)
					{
						while (producer.foundBuffer.Count < MIN_FOUND_BATCH_LENGTH && !poisonPill)
						{
							Monitor.Wait(emptyingBuffer);
						}

						if (poisonPill)
						{
							break;
						}

						ShipBatch();
					}
				}
			}



			/// <summary>
			/// Consumes new findings, packs them, and ships them using the FoundBatchFull event when appropriate.
			/// Should be accessed only with emtyingBuffer locked.
			/// </summary>
			private void ShipBatch()
			{
				if (!Monitor.IsEntered(emptyingBuffer))
				{
					throw new InvalidOperationException();
				}

				if (ShouldWait(out int ms))
				{
					Thread.Sleep(ms);
				}

				if (poisonPill)
				{
					return;
				}

				var batch = TakeBatch();

				FoundBatchFull?.Invoke(batch);

				lastBatchTime = DateTime.Now;
				infosSent += batch.Length;
			}

			/// <summary>
			/// Consumes batch until there are no new findings or batch is already at it's maximum length as set by MAX_FOUND_BATCH_LENGTH constant.
			/// </summary>
			/// <returns>Consumed batch of new findings</returns>
			private FileSystemInfo[] TakeBatch()
			{
				if (!Monitor.IsEntered(emptyingBuffer))
				{
					throw new InvalidOperationException();
				}

				var infosTaken = new List<FileSystemInfo>();
				while (producer.foundBuffer.TryTake(out FileSystemInfo info) && infosTaken.Count < MAX_FOUND_BATCH_LENGTH)
				{
					infosTaken.Add(info);
				}

				return infosTaken.ToArray();
			}

			/// <summary>
			/// Determines if the consumer should wait before shipping another batch. With slower devices may need to be adjusted.
			/// </summary>
			/// <param name="waitTime">How long should the consumer wait before shipping another batch.</param>
			/// <returns>True if consumer should wait, false otherwise.</returns>
			private bool ShouldWait(out int waitTime)
			{
				int shouldWait = infosSent <= 100 ?
					MIN_PAUSE_BETWEEN_BATCHES :
					MAX_PAUSE_BETWEEN_BATCHES;

				waitTime = shouldWait - (DateTime.Now - lastBatchTime).Milliseconds;
				return waitTime > 0;
			}


		}
	}
}
