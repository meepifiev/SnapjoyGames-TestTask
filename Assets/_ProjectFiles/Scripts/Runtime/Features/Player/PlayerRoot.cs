using System;
using MessagePipe;
using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Items;
using Project.Scripts.Runtime.Features.Messages;
using UnityEngine;
using VContainer;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _itemInspectionHolder;
        [SerializeField] private Transform _heldItemHolder;
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private InteractionSettings _interactionSettings;

        private IInputReader _inputReader;
        private ITimeProvider _timeProvider;

        private PlayerViewLock _viewLock;
        private PlayerMotor _motor;
        private PlayerLook _look;
        private PlayerFocus _focus;
        private PlayerCursor _cursor;
        private PlayerModeHandler _modeHandler;
        private HeldItemSlot _heldItemSlot;

        [Inject]
        private void Construct(
            IInputReader inputReader,
            ITimeProvider timeProvider,
            InteractionPipe interactionPipe,
            ISubscriber<M_PlayerMovementLockChanged> movementLockSubscriber,
            ISubscriber<M_PlayerLookLockChanged> lookLockSubscriber,
            ISubscriber<M_PlayerInteractionLockChanged> interactionLockSubscriber,
            ISubscriber<M_PlayerCursorVisibilityChanged> cursorVisibilitySubscriber)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));

            if (interactionPipe == null)
                throw new ArgumentNullException(nameof(interactionPipe));

            _viewLock = new PlayerViewLock();
            _cursor = new PlayerCursor(_settings);
            _modeHandler = new PlayerModeHandler(
                _viewLock,
                _cursor,
                movementLockSubscriber,
                lookLockSubscriber,
                interactionLockSubscriber,
                cursorVisibilitySubscriber);

            _heldItemSlot = new HeldItemSlot();
            _motor = new PlayerMotor(_characterController, _body, _settings, _viewLock);
            _look = new PlayerLook(_body, _camera, _settings, _viewLock);

            _focus = new PlayerFocus(
                _camera,
                new InteractionActor(
                    _camera,
                    _itemInspectionHolder,
                    _heldItemHolder,
                    _heldItemSlot),
                    interactionPipe,
                    _interactionSettings,
                    _viewLock);

            _inputReader.MoveChanged += _motor.SetMoveInput;
            _inputReader.LookChanged += _look.SetLookInput;

            _focus.Subscribe(_inputReader);

            _cursor.ApplyDefaultState();
        }

        private void Update()
        {
            float deltaTime = _timeProvider.DeltaTime;

            _focus.Tick(deltaTime);
            _inputReader.Read();

            _look.Tick();
            _motor.Tick(deltaTime);
        }

        private void OnDestroy()
        {
            _inputReader.MoveChanged -= _motor.SetMoveInput;
            _inputReader.LookChanged -= _look.SetLookInput;

            _focus.Unsubscribe(_inputReader);
            _modeHandler?.Dispose();
        }
    }
}
