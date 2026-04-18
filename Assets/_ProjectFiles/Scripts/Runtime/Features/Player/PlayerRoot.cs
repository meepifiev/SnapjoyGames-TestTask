using System;
using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
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

        private IInputReader _inputReader;
        private ITimeProvider _timeProvider;
        
        private PlayerViewLock _viewLock;
        private PlayerMotor _motor;
        private PlayerLook _look;

        public PlayerViewLock ViewLock => _viewLock;

        [Inject]
        public void Construct(IInputReader inputReader, ITimeProvider timeProvider)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));

            _viewLock = new PlayerViewLock();
            _motor = new PlayerMotor(_characterController, _body, _settings, _viewLock);
            _look = new PlayerLook(_body, _camera, _settings, _viewLock);

            _inputReader.MoveChanged += _motor.SetMoveInput;
            _inputReader.LookChanged += _look.SetLookInput;

            ApplyCursorState();
        }

        private void Update()
        {
            _inputReader.Read();

            _look.Tick();
            _motor.Tick(_timeProvider.DeltaTime);
        }

        private void OnDestroy()
        {
            _inputReader.MoveChanged -= _motor.SetMoveInput;
            _inputReader.LookChanged -= _look.SetLookInput;
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
