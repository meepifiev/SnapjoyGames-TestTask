using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class ItemSocket : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private ItemSocketSettings _settings;
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private PickupItem _initialItem;

        private PickupItem _item;

        public Transform ItemHolder => _itemHolder;
        public bool HasItem => _item != null;

        private void Awake()
        {
            if (_initialItem == null)
                return;

            Place(_initialItem);
        }

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_settings.PlaceInteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return HasItem == false &&
                   _settings != null &&
                   _itemHolder != null &&
                   actor.HeldItemSlot.HasItem &&
                   actor.HeldItemSlot.Item.CanPlaceInSocket &&
                   string.IsNullOrWhiteSpace(_settings.PlaceInteractionText) == false;
        }

        public void Press(InteractionActor actor)
        {
            if (CanInteract(actor) == false)
                return;

            PickupItem item = actor.HeldItemSlot.Item;
            actor.HeldItemSlot.Clear();

            item.PlaceToSocket(this);
        }

        public void Hold(InteractionActor actor, float deltaTime)
        {
        }

        public void Release(InteractionActor actor)
        {
        }

        public void Place(PickupItem item)
        {
            _item = item;
            _item.AttachToSocket(this);
        }

        public void Release(PickupItem item)
        {
            if (_item != item)
                return;

            _item = null;
            item.DetachFromSocket(this);
        }
    }
}
