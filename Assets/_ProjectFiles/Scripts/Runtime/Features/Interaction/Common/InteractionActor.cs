using Project.Scripts.Runtime.Features.Player;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public class InteractionActor
    {
        public InteractionActor(Transform transform, Transform viewPoint, PlayerViewLock viewLock)
        {
            Transform = transform;
            ViewPoint = viewPoint;
            ViewLock = viewLock;
        }

        public Transform Transform { get; }
        public Transform ViewPoint { get; }
        public PlayerViewLock ViewLock { get; }
    }
}
