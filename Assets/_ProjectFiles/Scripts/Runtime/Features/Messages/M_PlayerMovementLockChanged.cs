namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_PlayerMovementLockChanged
    {
        public M_PlayerMovementLockChanged(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public bool IsLocked { get; }
    }
}
