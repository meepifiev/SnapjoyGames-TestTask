using Project.Scripts.Runtime.Core.Input;
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
            PlayerViewLock viewLock,
            IInputReader inputReader,
            IItemInspectionOutput itemInspectionOutput)
        {
            Transform = transform;
            ViewPoint = viewPoint;
            ItemInspectionHolder = itemInspectionHolder;
            ViewLock = viewLock;
            InputReader = inputReader;
            ItemInspectionOutput = itemInspectionOutput;
        }

        public Transform Transform { get; }
        public Transform ViewPoint { get; }
        public Transform ItemInspectionHolder { get; }
        public PlayerViewLock ViewLock { get; }
        public IInputReader InputReader { get; }
        public IItemInspectionOutput ItemInspectionOutput { get; }
    }
}
