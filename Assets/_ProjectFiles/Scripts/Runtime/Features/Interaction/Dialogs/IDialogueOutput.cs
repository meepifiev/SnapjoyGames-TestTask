namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public interface IDialogueOutput
    {
        void Show(DialogueViewData viewData);
        void Hide();
    }
}
