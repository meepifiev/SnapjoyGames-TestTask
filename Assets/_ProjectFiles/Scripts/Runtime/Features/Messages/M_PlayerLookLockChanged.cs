namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_PlayerLookLockChanged
    {
        public M_PlayerLookLockChanged(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public bool IsLocked { get; }
    }
}
