using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Messages;
using VContainer.Unity;

namespace Project.Scripts.Runtime.UI.Interaction
{
    public class InteractionHintHandler : IStartable, IDisposable
    {
        private readonly InteractionHintPresenter _presenter;
        
        private readonly ISubscriber<M_InteractionHintShown> _shownSubscriber;
        private readonly ISubscriber<M_InteractionHintHidden> _hiddenSubscriber;
        
        private IDisposable _subscriptions;

        public InteractionHintHandler(
            InteractionHintPresenter presenter,
            ISubscriber<M_InteractionHintShown> shownSubscriber,
            ISubscriber<M_InteractionHintHidden> hiddenSubscriber)
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

        private void Show(M_InteractionHintShown message)
        {
            _presenter.Show(message.Hint);
        }

        private void Hide(M_InteractionHintHidden message)
        {
            _presenter.Hide();
        }
    }
}
