using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public class InteractionActor
    {
        public InteractionActor(
            Transform viewPoint,
            Transform itemInspectionHolder,
            Transform heldItemHolder,
            HeldItemSlot heldItemSlot)
        {
            ViewPoint = viewPoint;
            ItemInspectionHolder = itemInspectionHolder;
            HeldItemHolder = heldItemHolder;
            HeldItemSlot = heldItemSlot;
        }

        public Transform ViewPoint { get; }
        public Transform ItemInspectionHolder { get; }
        public Transform HeldItemHolder { get; }
        public HeldItemSlot HeldItemSlot { get; }
    }
}
