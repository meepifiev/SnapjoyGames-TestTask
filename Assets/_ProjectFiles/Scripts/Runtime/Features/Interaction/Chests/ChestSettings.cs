using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Chests
{
    [CreateAssetMenu(fileName = "ChestSettings", menuName = "Project/Interaction/Chests/Chest Settings")]
    public class ChestSettings : ScriptableObject
    {
        [field: Header("Unlock")]
        [field: SerializeField] public ItemDefinition RequiredItem { get; private set; }

        [field: Header("Text")]
        [field: SerializeField] public string OpenInteractionText { get; private set; }
        [field: SerializeField] public string LockedInteractionText { get; private set; }

        [field: Header("Lid")]
        [field: SerializeField] public ChestLidSettings LidSettings { get; private set; }
    }
}
