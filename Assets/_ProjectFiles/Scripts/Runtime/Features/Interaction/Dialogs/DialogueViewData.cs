using System.Collections.Generic;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public readonly struct DialogueViewData
    {
        public DialogueViewData(string speakerName, string line, IReadOnlyList<DialogueChoiceViewData> choices)
        {
            SpeakerName = speakerName;
            Line = line;
            Choices = choices;
        }

        public string SpeakerName { get; }
        public string Line { get; }
        public IReadOnlyList<DialogueChoiceViewData> Choices { get; }
    }
}
