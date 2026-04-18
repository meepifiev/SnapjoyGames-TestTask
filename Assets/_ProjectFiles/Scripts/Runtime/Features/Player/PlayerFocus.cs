using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerFocus
    {
        private readonly Transform _viewPoint;
        private readonly InteractionActor _actor;
        private readonly InteractionSettings _settings;
        private readonly PlayerViewLock _viewLock;

        private IInteractionTarget _target;
        
        private float _deltaTime;

        public PlayerFocus(
            Transform viewPoint,
            InteractionActor actor,
            InteractionSettings settings,
            PlayerViewLock viewLock)
        {
            _viewPoint = viewPoint;
            _actor = actor;
            _settings = settings;
            _viewLock = viewLock;
        }

        public void Subscribe(IInputReader inputReader)
        {
            inputReader.InteractionPressed += Press;
            inputReader.InteractionHeld += Hold;
            inputReader.InteractionReleased += Release;
        }

        public void Unsubscribe(IInputReader inputReader)
        {
            inputReader.InteractionPressed -= Press;
            inputReader.InteractionHeld -= Hold;
            inputReader.InteractionReleased -= Release;
        }

        public void Tick(float deltaTime)
        {
            _deltaTime = deltaTime;
            UpdateTarget();
        }

        private void Press()
        {
            if (_viewLock.CanInteract == false || _target == null || _target.CanInteract(_actor) == false)
                return;

            InteractionHint hint = _target.GetHint(_actor);
            Debug.Log(hint.ActionText);
            _target.Press(_actor);
        }

        private void Hold()
        {
            if (_viewLock.CanInteract == false || _target == null || _target.CanInteract(_actor) == false)
                return;

            _target.Hold(_actor, _deltaTime);
        }

        private void Release()
        {
            if (_target == null)
                return;

            _target.Release(_actor);
        }

        private void UpdateTarget()
        {
            Ray ray = new Ray(_viewPoint.position, _viewPoint.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _settings.Distance, _settings.TargetLayers) == false)
            {
                _target = null;
                return;
            }

            _target = hit.collider.GetComponentInParent<IInteractionTarget>();
        }
    }
}
