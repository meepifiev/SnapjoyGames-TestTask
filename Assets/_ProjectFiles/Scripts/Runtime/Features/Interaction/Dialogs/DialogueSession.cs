using System;
using System.Collections.Generic;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public class DialogueSession
    {
        private readonly NpcDefinition _npcDefinition;
        private readonly List<DialogueChoiceViewData> _choices;

        private int _nodeIndex;

        public DialogueSession(NpcDefinition npcDefinition)
        {
            _npcDefinition = npcDefinition;
            _choices = new List<DialogueChoiceViewData>();
            _nodeIndex = _npcDefinition.Dialogue.StartNodeIndex;
        }

        public DialogueViewData CreateViewData(Action finish, Action refresh)
        {
            DialogueNodeDefinition node = _npcDefinition.Dialogue.GetNode(_nodeIndex);
            
            _choices.Clear();

            if (node.Choices == null)
                return new DialogueViewData(_npcDefinition.DisplayName, node.Line, _choices);

            foreach (DialogueChoiceDefinition choice in node.Choices)
            {
                if (CanShow(choice) == false)
                    continue;

                DialogueChoiceDefinition choice1 = choice;
                _choices.Add(new DialogueChoiceViewData(
                    choice.Text,
                    () => Select(choice1, finish, refresh)));
            }

            return new DialogueViewData(_npcDefinition.DisplayName, node.Line, _choices);
        }

        private void Select(DialogueChoiceDefinition choice, Action finish, Action refresh)
        {
            if (choice.Transition.EndsDialogue || _npcDefinition.Dialogue.HasNode(choice.Transition.NextNodeIndex) == false)
            {
                finish?.Invoke();
                return;
            }

            _nodeIndex = choice.Transition.NextNodeIndex;
            
            refresh?.Invoke();
        }

        private bool CanShow(DialogueChoiceDefinition choice)
        {
            return choice != null &&
                   choice.Transition != null &&
                   string.IsNullOrWhiteSpace(choice.Text) == false;
        }
    }
}
