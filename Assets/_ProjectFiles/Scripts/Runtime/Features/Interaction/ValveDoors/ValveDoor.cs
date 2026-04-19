using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.ValveDoors
{
    public class ValveDoor : MonoBehaviour, IInteractionTarget
    {
        private const float ClosedValveAngle = 0f;

        [SerializeField] private ValveDoorSettings _settings;
        [SerializeField] private Transform _valve;
        [SerializeField] private Transform _door;

        private Quaternion _closedValveRotation;
        private Vector3 _closedDoorPosition;
        
        private float _currentValveAngle;
        
        private bool _isReturning;
        
        private ITimeProvider _timeProvider;

        private void Awake()
        {
            _closedValveRotation = _valve.localRotation;
            _closedDoorPosition = _door.localPosition;
        }

        private void Update()
        {
            if (CanReturnToClosedState() == false)
                return;

            ReturnToClosedState(_timeProvider.DeltaTime);
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_settings.RotateInteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return IsConfigured() &&
                   string.IsNullOrWhiteSpace(_settings.RotateInteractionText) == false;
        }

        public void Press(InteractionActor actor) { }

        public void Hold(InteractionActor actor, float deltaTime)
        {
            _timeProvider = actor.TimeProvider;
            _isReturning = false;
            Open(deltaTime);
        }

        public void Release(InteractionActor actor)
        {
            _timeProvider = actor.TimeProvider;
            _isReturning = true;
        }

        private void Open(float deltaTime)
        {
            float targetAngle = _settings.MotionSettings.MaxValveAngle;
            float angleStep = _settings.MotionSettings.OpeningSpeed * deltaTime;
            
            _currentValveAngle = Mathf.MoveTowards(_currentValveAngle, targetAngle, angleStep);

            ApplyMotion();
        }

        private void ReturnToClosedState(float deltaTime)
        {
            if (_currentValveAngle <= ClosedValveAngle)
                return;

            float angleStep = _settings.MotionSettings.ReturnSpeed * deltaTime;
            _currentValveAngle = Mathf.MoveTowards(_currentValveAngle, ClosedValveAngle, angleStep);

            ApplyMotion();
        }

        private void ApplyMotion()
        {
            float progress = _currentValveAngle / _settings.MotionSettings.MaxValveAngle;

            _valve.localRotation = _closedValveRotation * Quaternion.AngleAxis(_currentValveAngle, _settings.MotionSettings.ValveRotationAxis);

            _door.localPosition = _closedDoorPosition + _settings.MotionSettings.DoorOpenLocalOffset * progress;
        }

        private bool IsConfigured()
        {
            return _settings.MotionSettings.MaxValveAngle > ClosedValveAngle &&
                   _settings.MotionSettings.OpeningSpeed > ClosedValveAngle &&
                   _settings.MotionSettings.ReturnSpeed > ClosedValveAngle &&
                   _settings.MotionSettings.ValveRotationAxis != Vector3.zero;
        }

        private bool CanReturnToClosedState()
        {
            return _isReturning && IsConfigured();
        }
    }
}
