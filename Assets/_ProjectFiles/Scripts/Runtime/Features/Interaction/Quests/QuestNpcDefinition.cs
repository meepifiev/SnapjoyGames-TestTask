using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    [CreateAssetMenu(fileName = "QuestNpcDefinition", menuName = "Project/Interaction/Quests/Quest NPC Definition")]
    public class QuestNpcDefinition : ScriptableObject
    {
        [field: Header("NPC")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public string InteractionText { get; private set; }
        [field: SerializeField] public string CompleteInteractionText { get; private set; }

        [field: Header("Dialogue")]
        [field: SerializeField, TextArea] public string[] Lines { get; private set; }
        [field: SerializeField] public string ContinueChoiceText { get; private set; }
        [field: SerializeField] public string AcceptQuestChoiceText { get; private set; }
        [field: SerializeField, TextArea] public string NoItemLine { get; private set; }
        [field: SerializeField, TextArea] public string MissingItemLine { get; private set; }
        [field: SerializeField, TextArea] public string CompleteLine { get; private set; }
        [field: SerializeField] public string CloseChoiceText { get; private set; }

        [field: Header("Quest")]
        [field: SerializeField] public string QuestDescriptionFormat { get; private set; }
        [field: SerializeField] public ItemType[] ExcludedItemTypes { get; private set; }
    }
}
