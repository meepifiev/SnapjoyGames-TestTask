using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    [Serializable]
    public class DialogueChoiceDefinition
    {
        [SerializeField] private string _text;
        [SerializeField] private DialogueTransition _transition;

        public string Text => _text;
        public DialogueTransition Transition => _transition;
    }
}
