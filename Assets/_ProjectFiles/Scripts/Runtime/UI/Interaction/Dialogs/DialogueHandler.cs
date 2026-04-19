using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Messages;
using VContainer.Unity;

namespace Project.Scripts.Runtime.UI.Interaction.Dialogs
{
    public class DialogueHandler : IStartable, IDisposable
    {
        private readonly DialoguePresenter _presenter;
        
        private readonly ISubscriber<M_DialogueShown> _shownSubscriber;
        private readonly ISubscriber<M_DialogueHidden> _hiddenSubscriber;
        
        private IDisposable _subscriptions;

        public DialogueHandler(
            DialoguePresenter presenter,
            ISubscriber<M_DialogueShown> shownSubscriber,
            ISubscriber<M_DialogueHidden> hiddenSubscriber)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            
            _shownSubscriber = shownSubscriber ?? throw new ArgumentNullException(nameof(shownSubscriber));
            _hiddenSubscriber = hiddenSubscriber ?? throw new ArgumentNullException(nameof(hiddenSubscriber));
        }

        public void Start()
        {
            _subscriptions = DisposableBag.Create(
                _shownSubscriber.Subscribe(Show),
                _hiddenSubscriber.Subscribe(Hide));
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private void Show(M_DialogueShown message)
        {
            _presenter.Show(message.ViewData);
        }

        private void Hide(M_DialogueHidden message)
        {
            _presenter.Hide();
        }
    }
}
