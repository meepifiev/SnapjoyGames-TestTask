namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_PlayerInteractionLockChanged
    {
        public M_PlayerInteractionLockChanged(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public bool IsLocked { get; }
    }
}
