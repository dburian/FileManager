using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MultithreadedFileOperations
{
	class SearchWrapper : ISearchView
	{
		const int MIN_FOUND_BATCH_LENGTH = 1;
		const int MAX_FOUND_BATCH_LENGTH = 30 * MIN_FOUND_BATCH_LENGTH;

		readonly FileSystemNodeSearch searchEngine;
		readonly CancellationTokenSource cts;
		readonly TaskScheduler IOScheduler;

		BlockingCollection<FileSystemInfo> foundBuffer;

		Consumer consumer;

		public SearchWrapper(FileSystemNodeSearch searchEngine, CancellationTokenSource cts, Task task)
		{
			this.searchEngine = searchEngine;
			this.cts = cts;
			Task = task;

			IOScheduler = TaskScheduler.FromCurrentSynchronizationContext();

			foundBuffer = new BlockingCollection<FileSystemInfo>(MAX_FOUND_BATCH_LENGTH * 2);

			searchEngine.ExceptionRise += OnExceptionRaise; 
			searchEngine.NodeFound += OnNodeFound;
			Task.ContinueWith(_ => OnSearchDone(), new CancellationToken(), TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);

			consumer = new Consumer(this);
		}

		public SearchSettings Settings { get => searchEngine.Settings; }
		public Task Task { get; }

		public event OnSearchDoneDelegate SearchDone;
		public event OnExceptionRaiseDelegate ExceptionRaise;
		public event OnFoundBatchFullDelegate FoundBatchFull
		{
			add
			{
				consumer.FoundBatchFull += value;
			}
			remove
			{
				consumer.FoundBatchFull -= value;
			}
		}

		public void Stop()
		{
			cts.Cancel();
			consumer.Stop();
		}
		public void Start() => Task.Start(TaskScheduler.Default);

		public void Dispose()
		{
			cts.Cancel();
			consumer.Dispose();
		}
 
		void OnNodeFound(FileSystemInfo nodeFound)
		{
			if (foundBuffer.Count > MIN_FOUND_BATCH_LENGTH && Monitor.TryEnter(consumer.emptyingBuffer))
			{
				Monitor.Pulse(consumer.emptyingBuffer);
				Monitor.Exit(consumer.emptyingBuffer);
			}

			foundBuffer.Add(nodeFound);
		}

		void OnExceptionRaise(FileOperationException e)
		{
			ExceptionRaise?.Invoke(e);
		}


		void OnSearchDone()
		{
			consumer.Flush();
			consumer.Dispose();

			SearchDone?.Invoke();
		}



		class Consumer : IDisposable
		{
			const int MIN_PAUSE_BETWEEN_BATCHES = 20;
			const int MAX_PAUSE_BETWEEN_BATCHES = 800;

			public object emptyingBuffer;

			volatile bool poisonPill;

			int infosSent = 0;
			DateTime lastBatchTime;
			Thread cleaningThread;

			SearchWrapper producer;


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

			public void Flush()
			{
				lock (emptyingBuffer)
				{
					while (producer.foundBuffer.Count > 0 && !poisonPill)
						ShipBatch();
				}
			}

			public void Consume()
			{
				lock (emptyingBuffer)
				{
					while (!poisonPill)
					{
						while (producer.foundBuffer.Count < MIN_FOUND_BATCH_LENGTH && !poisonPill) Monitor.Wait(emptyingBuffer);
						if (poisonPill) break;

						ShipBatch();
					}
				}
			}

			public void Stop()
			{
				poisonPill = true;

				lock (emptyingBuffer)
				{
					Monitor.Pulse(emptyingBuffer);
				}
			}

			//Should be accessed only with emptyingBuffer locked...
			void ShipBatch()
			{
				if (!Monitor.IsEntered(emptyingBuffer)) throw new InvalidOperationException();
				
				int ms;
				if (ShouldWait(out ms)) Thread.Sleep(ms);
				if (poisonPill) return;

				var batch = TakeBatch();

				FoundBatchFull?.Invoke(batch);

				lastBatchTime = DateTime.Now;
				infosSent += batch.Length;
			}

			//Should be accessed only with emptyingBuffer locked...
			FileSystemInfo[] TakeBatch()
			{
				if (!Monitor.IsEntered(emptyingBuffer)) throw new InvalidOperationException();

				var infosTaken = new List<FileSystemInfo>();
				FileSystemInfo info;
				while (producer.foundBuffer.TryTake(out info) && infosTaken.Count < MAX_FOUND_BATCH_LENGTH)
				{
					infosTaken.Add(info);
				}

				return infosTaken.ToArray();
			}

			bool ShouldWait(out int waitTime)
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
