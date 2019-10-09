using System;
using System.Threading;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Slim synchronization primitive. Freeze lock rules out moving locks. There can be more threads moving at the same time.
	/// </summary>
	internal class MoveFreezeLockSlim : IDisposable
	{
		private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		public int CurrentMoveCount => rwLock.CurrentReadCount;

		public int WaitingMoveCount => rwLock.WaitingReadCount;
		public int WaitingFreezeCount => rwLock.WaitingWriteCount;

		public bool IsMoveLockHeld => rwLock.IsReadLockHeld;
		public bool IsFreezeLockHeld => rwLock.IsWriteLockHeld;

		public void EnterMoveLock()
		{
			rwLock.EnterReadLock();
		}

		public void ExitMoveLock()
		{
			rwLock.ExitReadLock();
		}

		public bool TryEnterMoveLock(int millisecondsTimeout)
		{
			return rwLock.TryEnterReadLock(millisecondsTimeout);
		}

		public void EnterFreezeLock()
		{
			rwLock.EnterWriteLock();
		}

		public void ExitFreezeLock()
		{
			rwLock.ExitWriteLock();
		}

		public bool TryEnterFreezeLock(int millisecondsTimeout)
		{
			return rwLock.TryEnterWriteLock(millisecondsTimeout);
		}

		public void Dispose()
		{
			rwLock.Dispose();
		}
	}
}
