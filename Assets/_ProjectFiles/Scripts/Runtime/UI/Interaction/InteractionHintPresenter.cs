using System;
using Project.Scripts.Runtime.Features.Interaction.Common;

namespace Project.Scripts.Runtime.UI.Interaction
{
    public class InteractionHintPresenter
    {
        private readonly IInteractionHintView _view;
        private readonly InteractionHintSettings _settings;

        public InteractionHintPresenter(
            IInteractionHintView view,
            InteractionHintSettings settings)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Show(InteractionHint hint)
        {
            _view.Show($"{_settings.InteractionKeyLabel}{_settings.Separator}{hint.ActionText}");
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
