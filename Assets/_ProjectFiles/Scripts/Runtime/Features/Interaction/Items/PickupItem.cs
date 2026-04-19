using DG.Tweening;
using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class PickupItem : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private ItemDefinition _definition;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isInspecting;
        private bool _isTransitioning;
        private bool _isHeld;

        private Transform _initialParent;

        private Vector3 _initialLocalPosition;
        private Quaternion _initialLocalRotation;
        private Vector3 _initialLocalScale;

        private bool _initialIsKinematic;
        private bool _initialUseGravity;

        private Tween _transitionTween;
        
        private InspectableItemRotation _rotation;
        
        private ItemSocket _socket;

        public ItemDefinition Definition => _definition;
        public bool CanPlaceInSocket => _isHeld && _isTransitioning == false;

        private void OnDestroy()
        {
            _transitionTween?.Kill();
            DisableRotation();
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            if (_isInspecting)
                return new InteractionHint(_definition.TakeInteractionText);

            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _isHeld == false &&
                   (_isInspecting || actor.HeldItemSlot.HasItem == false) &&
                   HasAvailableInteractionText();
        }

        public void Press(InteractionActor actor)
        {
            if (_isTransitioning)
                return;

            if (_isInspecting)
            {
                PickUp(actor);
                return;
            }

            StartInspection(actor);
        }

        public void Hold(InteractionActor actor, float deltaTime)
        {
        }

        public void Release(InteractionActor actor)
        {
        }

        private void StartInspection(InteractionActor actor)
        {
            _isInspecting = true;
            
            actor.ViewLock.LockMovement();
            actor.ViewLock.LockLook();
            actor.ItemInspectionOutput.Show(_definition);
            
            SaveInitialPose();
            ReleaseSocket();
            
            SaveRigidbodyState();
            SetInspectionRigidbodyState();
            
            EnableRotation(actor);
            MoveToInspectionPose(actor.ItemInspectionHolder);
        }

        private void PickUp(InteractionActor actor)
        {
            if (actor.HeldItemSlot.CanHold(this) == false)
                return;

            _isInspecting = false;
            _isHeld = true;
            
            actor.HeldItemSlot.Hold(this);
            actor.ItemInspectionOutput.Hide();
            
            DisableRotation();
            MoveToHeldPose(actor);
        }

        public void PlaceToSocket(ItemSocket socket)
        {
            _isHeld = false;
            socket.Place(this);

            MoveToSocketPose(socket);
        }

        public void AttachToSocket(ItemSocket socket)
        {
            _socket = socket;
        }

        public void DetachFromSocket(ItemSocket socket)
        {
            if (_socket != socket)
                return;

            _socket = null;
        }

        public void Consume()
        {
            _transitionTween?.Kill();
            DisableRotation();
            ReleaseSocket();

            Destroy(gameObject);
        }

        private void SaveInitialPose()
        {
            _initialParent = transform.parent;
            _initialLocalPosition = transform.localPosition;
            _initialLocalRotation = transform.localRotation;
            _initialLocalScale = transform.localScale;
        }

        private void MoveToInspectionPose(Transform itemInspectionHolder)
        {
            transform.SetParent(itemInspectionHolder, true);

            PlayTransition(
                Vector3.zero,
                Quaternion.Euler(_definition.InspectionSettings.LocalEulerAngles),
                _definition.InspectionSettings.LocalScale,
                null);
        }

        private void MoveToInitialPose(InteractionActor actor)
        {
            transform.SetParent(_initialParent, true);

            PlayTransition(
                _initialLocalPosition,
                _initialLocalRotation,
                _initialLocalScale,
                () =>
                {
                    RestoreRigidbodyState();
                    actor.ViewLock.UnlockLook();
                    actor.ViewLock.UnlockMovement();
                });
        }

        private void MoveToHeldPose(InteractionActor actor)
        {
            transform.SetParent(actor.HeldItemHolder, true);

            PlayTransition(
                Vector3.zero,
                Quaternion.Euler(_definition.HeldSettings.LocalEulerAngles),
                _definition.HeldSettings.LocalScale,
                () =>
                {
                    actor.ViewLock.UnlockLook();
                    actor.ViewLock.UnlockMovement();
                },
                _definition.HeldSettings.TransitionDuration,
                _definition.HeldSettings.TransitionEase);
        }

        private void MoveToSocketPose(ItemSocket socket)
        {
            transform.SetParent(socket.ItemHolder, true);

            PlayTransition(
                Vector3.zero,
                Quaternion.identity,
                _initialLocalScale,
                RestoreRigidbodyState);
        }

        private void PlayTransition(
            Vector3 localPosition,
            Quaternion localRotation,
            Vector3 localScale,
            TweenCallback onComplete)
        {
            PlayTransition(
                localPosition,
                localRotation,
                localScale,
                onComplete,
                _definition.InspectionSettings.TransitionDuration,
                _definition.InspectionSettings.TransitionEase);
        }

        private void PlayTransition(
            Vector3 localPosition,
            Quaternion localRotation,
            Vector3 localScale,
            TweenCallback onComplete,
            float duration,
            Ease ease)
        {
            _transitionTween?.Kill();
            _isTransitioning = true;

            _transitionTween = DOTween.Sequence()
                .Join(transform.DOLocalMove(localPosition, duration))
                .Join(transform.DOLocalRotateQuaternion(localRotation, duration))
                .Join(transform.DOScale(localScale, duration))
                .SetEase(ease)
                .OnComplete(() =>
                {
                    _isTransitioning = false;
                    onComplete?.Invoke();
                });
        }

        private void SaveRigidbodyState()
        {
            _initialIsKinematic = _rigidbody.isKinematic;
            _initialUseGravity = _rigidbody.useGravity;
        }

        private void SetInspectionRigidbodyState()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        private void RestoreRigidbodyState()
        {
            _rigidbody.isKinematic = _initialIsKinematic;
            _rigidbody.useGravity = _initialUseGravity;
        }

        private void EnableRotation(InteractionActor actor)
        {
            _rotation = new InspectableItemRotation(
                transform,
                actor.ViewPoint,
                actor.InputReader,
                _definition.InspectionSettings.RotationSpeed);

            _rotation.Enable();
        }

        private void DisableRotation()
        {
            _rotation?.Disable();
            _rotation = null;
        }

        private void ReleaseSocket()
        {
            _socket?.Release(this);
        }

        private bool HasAvailableInteractionText()
        {
            if (_isInspecting)
                return string.IsNullOrWhiteSpace(_definition.TakeInteractionText) == false;

            return string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }
    }
}
