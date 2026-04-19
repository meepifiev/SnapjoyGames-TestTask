using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerCursor
    {
        private const int NoRequests = 0;
        private const int RequestStep = 1;

        private readonly PlayerSettings _settings;
        private int _visibleRequestCount;

        public PlayerCursor(PlayerSettings settings)
        {
            _settings = settings;
        }

        public void ApplyDefaultState()
        {
            if (_settings.DefaultLockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void RequestVisible()
        {
            _visibleRequestCount += RequestStep;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ReleaseVisible()
        {
            if (_visibleRequestCount <= NoRequests)
                return;

            _visibleRequestCount -= RequestStep;

            if (_visibleRequestCount == NoRequests)
                ApplyDefaultState();
        }
    }
}
