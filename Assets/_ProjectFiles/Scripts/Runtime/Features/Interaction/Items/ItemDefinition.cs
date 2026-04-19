using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [CreateAssetMenu(fileName = "ItemDefinition", menuName = "Project/Interaction/Items/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [field: Header("Type")]
        [field: SerializeField] public ItemType Type { get; private set; }

        [field: Header("Text")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public string InteractionText { get; private set; }
        [field: SerializeField] public string TakeInteractionText { get; private set; }

        [field: Header("Inspection")]
        [field: SerializeField] public ItemInspectionSettings InspectionSettings { get; private set; }

        [field: Header("Held")]
        [field: SerializeField] public HeldItemSettings HeldSettings { get; private set; }
    }
}
