using DG.Tweening;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Chests
{
    public class Chest : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private ChestSettings _settings;
        [SerializeField] private Transform _lid;

        private bool _isOpen;
        private bool _isTransitioning;
        
        private Tween _openTween;

        private void OnDestroy()
        {
            _openTween?.Kill();
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            if (HasRequiredItem(actor))
                return new InteractionHint(_settings.OpenInteractionText);

            return new InteractionHint(_settings.LockedInteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _isOpen == false &&
                   _isTransitioning == false &&
                   HasAvailableInteractionText(actor);
        }

        public void Press(InteractionActor actor)
        {
            if (CanInteract(actor) == false || HasRequiredItem(actor) == false)
                return;

            PickupItem item = actor.HeldItemSlot.Item;
            
            actor.HeldItemSlot.Clear();
            
            item.Consume();

            Open();
        }

        public void Hold(InteractionActor actor, float deltaTime) { }

        public void Release(InteractionActor actor) { }

        private void Open()
        {
            _isTransitioning = true;

            _openTween?.Kill();
            
            _openTween = _lid
                .DOLocalRotate(_settings.LidSettings.OpenLocalEulerAngles, _settings.LidSettings.TransitionDuration)
                .SetEase(_settings.LidSettings.TransitionEase)
                .OnComplete(() =>
                {
                    _isOpen = true;
                    _isTransitioning = false;
                });
        }

        private bool HasRequiredItem(InteractionActor actor)
        {
            return actor.HeldItemSlot.HasItem &&
                   actor.HeldItemSlot.Item.Definition == _settings.RequiredItem;
        }

        private bool HasAvailableInteractionText(InteractionActor actor)
        {
            if (HasRequiredItem(actor))
                return string.IsNullOrWhiteSpace(_settings.OpenInteractionText) == false;

            return string.IsNullOrWhiteSpace(_settings.LockedInteractionText) == false;
        }
    }
}
