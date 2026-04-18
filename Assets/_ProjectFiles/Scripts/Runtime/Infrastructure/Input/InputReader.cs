using System;
using Project.Scripts.Runtime.Core.Input;
using UnityEngine;

namespace Project.Scripts.Runtime.Infrastructure.Controls
{
    public class InputReader : IInputReader
    {
        private const float InputThreshold = 0.001f;
        private const int PrimaryMouseButton = 0;

        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string MouseXAxis = "Mouse X";
        private const string MouseYAxis = "Mouse Y";

        private const KeyCode InteractionKey = KeyCode.E;

        public event Action<Vector2> MoveChanged;
        public event Action<Vector2> LookChanged;

        public event Action<Vector2> PointerDeltaChanged;

        public event Action InteractionPressed;
        public event Action InteractionHeld;
        public event Action InteractionReleased;

        public event Action PrimaryPointerPressed;
        public event Action PrimaryPointerHeld;
        public event Action PrimaryPointerReleased;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        public Vector2 PointerDelta { get; private set; }

        public bool IsInteractionHeld { get; private set; }
        public bool IsPrimaryPointerHeld { get; private set; }

        public void Read()
        {
            ReadMove();
            ReadLook();
            
            ReadInteraction();
            
            ReadPrimaryPointer();
        }

        private void ReadMove()
        {
            Vector2 currentMove = new Vector2(
                Input.GetAxisRaw(HorizontalAxis),
                Input.GetAxisRaw(VerticalAxis));

            if (HasChanged(Move, currentMove) == false)
                return;

            Move = currentMove;
            
            MoveChanged?.Invoke(Move);
        }

        private void ReadLook()
        {
            Vector2 currentLook = new Vector2(
                Input.GetAxis(MouseXAxis),
                Input.GetAxis(MouseYAxis));

            if (HasChanged(Look, currentLook) == false)
                return;

            Look = currentLook;
            PointerDelta = currentLook;

            LookChanged?.Invoke(Look);
            PointerDeltaChanged?.Invoke(PointerDelta);
        }

        private void ReadInteraction()
        {
            IsInteractionHeld = Input.GetKey(InteractionKey);

            if (Input.GetKeyDown(InteractionKey))
                InteractionPressed?.Invoke();

            if (IsInteractionHeld)
                InteractionHeld?.Invoke();

            if (Input.GetKeyUp(InteractionKey))
                InteractionReleased?.Invoke();
        }

        private void ReadPrimaryPointer()
        {
            IsPrimaryPointerHeld = Input.GetMouseButton(PrimaryMouseButton);

            if (Input.GetMouseButtonDown(PrimaryMouseButton))
                PrimaryPointerPressed?.Invoke();

            if (IsPrimaryPointerHeld)
                PrimaryPointerHeld?.Invoke();

            if (Input.GetMouseButtonUp(PrimaryMouseButton))
                PrimaryPointerReleased?.Invoke();
        }

        private bool HasChanged(Vector2 previousValue, Vector2 currentValue)
        {
            return (previousValue - currentValue).sqrMagnitude > InputThreshold * InputThreshold;
        }
    }
}
