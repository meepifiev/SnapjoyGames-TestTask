using System;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public readonly struct DialogueChoiceViewData
    {
        public DialogueChoiceViewData(string text, Action select)
        {
            Text = text;
            Select = select;
        }

        public string Text { get; }
        public Action Select { get; }
    }
}
