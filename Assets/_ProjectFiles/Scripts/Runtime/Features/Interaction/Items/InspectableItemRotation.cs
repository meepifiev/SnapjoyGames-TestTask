using Project.Scripts.Runtime.Core.Input;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class InspectableItemRotation
    {
        private readonly Transform _target;
        private readonly Transform _rotationFrame;
        private readonly IInputReader _inputReader;
        
        private readonly float _rotationSpeed;

        private bool _isEnabled;
        private bool _isPointerHeld;

        public InspectableItemRotation(
            Transform target,
            Transform rotationFrame,
            IInputReader inputReader,
            float rotationSpeed)
        {
            _target = target;
            _rotationFrame = rotationFrame;
            _inputReader = inputReader;
            _rotationSpeed = rotationSpeed;
        }

        public void Enable()
        {
            if (_isEnabled)
                return;

            _inputReader.PrimaryPointerPressed += OnPointerPressed;
            _inputReader.PrimaryPointerReleased += OnPointerReleased;
            _inputReader.PointerDeltaChanged += Rotate;

            _isEnabled = true;
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;

            _inputReader.PrimaryPointerPressed -= OnPointerPressed;
            _inputReader.PrimaryPointerReleased -= OnPointerReleased;
            _inputReader.PointerDeltaChanged -= Rotate;

            _isPointerHeld = false;
            _isEnabled = false;
        }

        private void OnPointerPressed()
        {
            _isPointerHeld = true;
        }

        private void OnPointerReleased()
        {
            _isPointerHeld = false;
        }

        private void Rotate(Vector2 pointerDelta)
        {
            if (_isPointerHeld == false)
                return;

            _target.Rotate(_rotationFrame.up, -pointerDelta.x * _rotationSpeed, Space.World);
            _target.Rotate(_rotationFrame.right, pointerDelta.y * _rotationSpeed, Space.World);
        }
    }
}
