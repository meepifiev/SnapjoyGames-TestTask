using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerLook
    {
        private const float CameraYaw = 0f;
        private const float CameraRoll = 0f;
        private const float MaxPositiveEulerAngle = 180f;
        private const float FullEulerRotation = 360f;

        private readonly Transform _body;
        private readonly Transform _camera;
        private readonly PlayerSettings _settings;
        private readonly PlayerViewLock _viewLock;

        private Vector2 _lookInput;
        private float _pitch;

        public PlayerLook(
            Transform body,
            Transform camera,
            PlayerSettings settings,
            PlayerViewLock viewLock)
        {
            _body = body;
            _camera = camera;
            _settings = settings;
            _viewLock = viewLock;

            _pitch = NormalizePitch(_camera.localEulerAngles.x);
        }

        public void SetLookInput(Vector2 lookInput)
        {
            _lookInput = lookInput;
        }

        public void Tick()
        {
            if (_viewLock.CanLook == false)
                return;

            Vector2 lookDelta = _lookInput * _settings.LookSensitivity;

            _body.Rotate(Vector3.up * lookDelta.x);

            _pitch = Mathf.Clamp(
                _pitch - lookDelta.y,
                _settings.MinPitch,
                _settings.MaxPitch);

            _camera.localRotation = Quaternion.Euler(_pitch, CameraYaw, CameraRoll);
        }

        private float NormalizePitch(float pitch)
        {
            if (pitch > MaxPositiveEulerAngle)
                return pitch - FullEulerRotation;

            return pitch;
        }
    }
}
