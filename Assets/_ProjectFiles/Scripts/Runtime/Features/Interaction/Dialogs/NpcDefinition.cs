using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    [CreateAssetMenu(fileName = "NpcDefinition", menuName = "Project/Interaction/Dialogs/NPC Definition")]
    public class NpcDefinition : ScriptableObject
    {
        [field: Header("Text")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public string InteractionText { get; private set; }

        [field: Header("Dialogue")]
        [field: SerializeField] public DialogueDefinition Dialogue { get; private set; }
    }
}
