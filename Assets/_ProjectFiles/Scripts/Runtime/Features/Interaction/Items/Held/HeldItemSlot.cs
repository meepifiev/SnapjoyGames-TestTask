namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class HeldItemSlot
    {
        public PickupItem Item { get; private set; }
        public bool HasItem => Item != null;

        public bool CanHold(PickupItem item)
        {
            return item != null && HasItem == false;
        }

        public void Hold(PickupItem item)
        {
            Item = item;
        }

        public void Clear()
        {
            Item = null;
        }
    }
}
