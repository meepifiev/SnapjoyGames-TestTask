namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_PlayerCursorVisibilityChanged
    {
        public M_PlayerCursorVisibilityChanged(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsVisible { get; }
    }
}
