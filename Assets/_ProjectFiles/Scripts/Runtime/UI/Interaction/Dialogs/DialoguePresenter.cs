using System;
using Project.Scripts.Runtime.Features.Interaction.Dialogs;

namespace Project.Scripts.Runtime.UI.Interaction.Dialogs
{
    public class DialoguePresenter
    {
        private readonly IDialogueView _view;

        public DialoguePresenter(IDialogueView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Show(DialogueViewData viewData)
        {
            _view.Show(viewData);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
