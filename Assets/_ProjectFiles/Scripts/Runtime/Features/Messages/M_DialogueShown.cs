using Project.Scripts.Runtime.Features.Interaction.Dialogs;

namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_DialogueShown
    {
        public M_DialogueShown(DialogueViewData viewData)
        {
            ViewData = viewData;
        }

        public DialogueViewData ViewData { get; }
    }
}
