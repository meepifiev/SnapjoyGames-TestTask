using System;
using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;
using VContainer;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _camera;
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private InteractionSettings _interactionSettings;

        private IInputReader _inputReader;
        private ITimeProvider _timeProvider;

        private PlayerViewLock _viewLock;
        private PlayerMotor _motor;
        private PlayerLook _look;
        private PlayerFocus _focus;

        public PlayerViewLock ViewLock => _viewLock;

        [Inject]
        public void Construct(
            IInputReader inputReader,
            ITimeProvider timeProvider,
            IInteractionHintOutput interactionHintOutput,
            IItemInspectionOutput itemInspectionOutput)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));

            _viewLock = new PlayerViewLock();
            _motor = new PlayerMotor(_characterController, _body, _settings, _viewLock);
            _look = new PlayerLook(_body, _camera, _settings, _viewLock);
            _focus = new PlayerFocus(
                _camera,
                new InteractionActor(_body, _camera, _viewLock, itemInspectionOutput),
                _interactionSettings,
                _viewLock,
                interactionHintOutput);

            _inputReader.MoveChanged += _motor.SetMoveInput;
            _inputReader.LookChanged += _look.SetLookInput;

            _focus.Subscribe(_inputReader);

            ApplyCursorState();
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
        }

        private void ApplyCursorState()
        {
            if (_settings.DefaultLockCursor == false)
                return;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
