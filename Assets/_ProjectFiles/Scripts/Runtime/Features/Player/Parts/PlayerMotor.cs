using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerMotor
    {
        private const float MaxMoveInputMagnitude = 1f;
        private const float LocalDirectionHeight = 0f;
        private const float GroundedVelocityLimit = 0f;

        private readonly CharacterController _characterController;
        private readonly Transform _body;
        private readonly PlayerSettings _settings;
        private readonly PlayerViewLock _viewLock;

        private Vector2 _moveInput;
        private float _verticalVelocity;

        public PlayerMotor(
            CharacterController characterController,
            Transform body,
            PlayerSettings settings,
            PlayerViewLock viewLock)
        {
            _characterController = characterController;
            _body = body;
            _settings = settings;
            _viewLock = viewLock;
        }

        public void SetMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
        }

        public void Tick(float deltaTime)
        {
            UpdateVerticalVelocity(deltaTime);

            Vector3 horizontalVelocity = Vector3.zero;

            if (_viewLock.CanMove)
                horizontalVelocity = GetHorizontalVelocity(_moveInput);

            Vector3 velocity = horizontalVelocity + Vector3.up * _verticalVelocity;
            
            _characterController.Move(velocity * deltaTime);
        }

        private Vector3 GetHorizontalVelocity(Vector2 moveInput)
        {
            Vector2 clampedInput = Vector2.ClampMagnitude(moveInput, MaxMoveInputMagnitude);
            Vector3 localDirection = new Vector3(clampedInput.x, LocalDirectionHeight, clampedInput.y);
            Vector3 worldDirection = _body.TransformDirection(localDirection);

            return worldDirection * _settings.MoveSpeed;
        }

        private void UpdateVerticalVelocity(float deltaTime)
        {
            if (_characterController.isGrounded && _verticalVelocity < GroundedVelocityLimit)
            {
                _verticalVelocity = _settings.GroundedVerticalVelocity;
                return;
            }

            _verticalVelocity += _settings.Gravity * deltaTime;
        }
    }
}
