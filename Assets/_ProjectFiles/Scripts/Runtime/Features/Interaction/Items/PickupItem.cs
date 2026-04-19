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

        private Transform _initialParent;

        private Vector3 _initialLocalPosition;
        private Quaternion _initialLocalRotation;
        private Vector3 _initialLocalScale;

        private bool _initialIsKinematic;
        private bool _initialUseGravity;

        private Tween _transitionTween;
        private InspectableItemRotation _rotation;

        public ItemDefinition Definition => _definition;

        private void OnDestroy()
        {
            _transitionTween?.Kill();
            DisableRotation();
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _definition != null &&
                   _definition.InspectionSettings != null &&
                   string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }

        public void Press(InteractionActor actor)
        {
            if (_isTransitioning)
                return;

            if (_isInspecting)
            {
                StopInspection(actor);
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
            SaveRigidbodyState();
            SetInspectionRigidbodyState();
            EnableRotation(actor);
            MoveToInspectionPose(actor.ItemInspectionHolder);
        }

        private void StopInspection(InteractionActor actor)
        {
            _isInspecting = false;
            actor.ItemInspectionOutput.Hide();
            DisableRotation();
            MoveToInitialPose(actor);
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

        private void PlayTransition(
            Vector3 localPosition,
            Quaternion localRotation,
            Vector3 localScale,
            TweenCallback onComplete)
        {
            _transitionTween?.Kill();
            _isTransitioning = true;

            _transitionTween = DOTween.Sequence()
                .Join(transform.DOLocalMove(localPosition, _definition.InspectionSettings.TransitionDuration))
                .Join(transform.DOLocalRotateQuaternion(localRotation, _definition.InspectionSettings.TransitionDuration))
                .Join(transform.DOScale(localScale, _definition.InspectionSettings.TransitionDuration))
                .SetEase(_definition.InspectionSettings.TransitionEase)
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
    }
}
