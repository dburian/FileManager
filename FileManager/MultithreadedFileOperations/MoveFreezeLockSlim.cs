using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultithreadedFileOperations
{
	class MoveFreezeLockSlim : IDisposable
	{
		ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		public int CurrentMoveCount { get => rwLock.CurrentReadCount; }

		public int WaitingMoveCount { get => rwLock.WaitingReadCount; }
		public int WaitingFreezeCount { get => rwLock.WaitingWriteCount; }

		public bool IsMoveLockHeld { get => rwLock.IsReadLockHeld; }
		public bool IsFreezeLockHeld { get => rwLock.IsWriteLockHeld; }

		public void EnterMoveLock() => rwLock.EnterReadLock();
		public void ExitMoveLock() => rwLock.ExitReadLock();
		public bool TryEnterMoveLock(int millisecondsTimeout) => rwLock.TryEnterReadLock(millisecondsTimeout);

		public void EnterFreezeLock() => rwLock.EnterWriteLock();
		public void ExitFreezeLock() => rwLock.ExitWriteLock();
		public bool TryEnterFreezeLock(int millisecondsTimeout) => rwLock.TryEnterWriteLock(millisecondsTimeout);

		public void Dispose() => rwLock.Dispose();
	}
}
