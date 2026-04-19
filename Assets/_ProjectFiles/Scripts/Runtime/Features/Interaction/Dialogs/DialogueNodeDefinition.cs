using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    [Serializable]
    public class DialogueNodeDefinition
    {
        [SerializeField, TextArea] private string _line;
        [SerializeField] private DialogueChoiceDefinition[] _choices;

        public string Line => _line;
        public DialogueChoiceDefinition[] Choices => _choices;
    }
}
