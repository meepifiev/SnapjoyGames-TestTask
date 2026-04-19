using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [CreateAssetMenu(fileName = "ItemSocketSettings", menuName = "Project/Interaction/Items/Item Socket Settings")]
    public class ItemSocketSettings : ScriptableObject
    {
        [field: SerializeField] public string PlaceInteractionText { get; private set; }
    }
}
