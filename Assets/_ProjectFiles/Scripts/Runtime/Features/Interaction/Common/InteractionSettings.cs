using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    [CreateAssetMenu(
        fileName = "InteractionSettings",
        menuName = "Project/Interaction/Interaction Settings")]
    public class InteractionSettings : ScriptableObject
    {
        [field: SerializeField, Min(0f)] public float Distance { get; private set; } = 3f;
        [field: SerializeField] public LayerMask TargetLayers { get; private set; } = ~0;
    }
}
