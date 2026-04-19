using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Items;
using Project.Scripts.Runtime.Features.Player;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public class InteractionActor
    {
        public InteractionActor(
            Transform transform,
            Transform viewPoint,
            Transform itemInspectionHolder,
            Transform heldItemHolder,
            HeldItemSlot heldItemSlot,
            PlayerViewLock viewLock,
            IInputReader inputReader,
            ITimeProvider timeProvider,
            IItemInspectionOutput itemInspectionOutput)
        {
            Transform = transform;
            ViewPoint = viewPoint;
            ItemInspectionHolder = itemInspectionHolder;
            HeldItemHolder = heldItemHolder;
            HeldItemSlot = heldItemSlot;
            ViewLock = viewLock;
            InputReader = inputReader;
            TimeProvider = timeProvider;
            ItemInspectionOutput = itemInspectionOutput;
        }

        public Transform Transform { get; }
        public Transform ViewPoint { get; }
        public Transform ItemInspectionHolder { get; }
        public Transform HeldItemHolder { get; }
        public HeldItemSlot HeldItemSlot { get; }
        public PlayerViewLock ViewLock { get; }
        public IInputReader InputReader { get; }
        public ITimeProvider TimeProvider { get; }
        public IItemInspectionOutput ItemInspectionOutput { get; }
    }
}
