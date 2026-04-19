namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerViewLock
    {
        private const int NoLocks = 0;
        private const int LockStep = 1;

        private int _movementLockCount;
        private int _lookLockCount;
        private int _interactionLockCount;

        public bool CanMove => _movementLockCount == NoLocks;
        public bool CanLook => _lookLockCount == NoLocks;
        public bool CanInteract => _interactionLockCount == NoLocks;

        public void LockMovement()
        {
            _movementLockCount += LockStep;
        }

        public void UnlockMovement()
        {
            _movementLockCount = GetReducedLockCount(_movementLockCount);
        }

        public void LockLook()
        {
            _lookLockCount += LockStep;
        }

        public void UnlockLook()
        {
            _lookLockCount = GetReducedLockCount(_lookLockCount);
        }

        public void LockInteraction()
        {
            _interactionLockCount += LockStep;
        }

        public void UnlockInteraction()
        {
            _interactionLockCount = GetReducedLockCount(_interactionLockCount);
        }

        private int GetReducedLockCount(int lockCount)
        {
            if (lockCount <= NoLocks)
                return NoLocks;

            return lockCount - LockStep;
        }
    }
}
