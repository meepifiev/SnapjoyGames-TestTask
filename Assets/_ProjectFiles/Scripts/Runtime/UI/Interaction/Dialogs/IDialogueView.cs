using Project.Scripts.Runtime.Features.Interaction.Dialogs;

namespace Project.Scripts.Runtime.UI.Interaction.Dialogs
{
    public interface IDialogueView
    {
        void Show(DialogueViewData viewData);
        void Hide();
    }
}
