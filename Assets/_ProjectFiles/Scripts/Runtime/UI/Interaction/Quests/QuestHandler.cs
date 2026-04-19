using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Messages;
using VContainer.Unity;

namespace Project.Scripts.Runtime.UI.Interaction.Quests
{
    public class QuestHandler : IStartable, IDisposable
    {
        private readonly QuestPresenter _presenter;
        
        private readonly ISubscriber<M_QuestShown> _shownSubscriber;
        
        private IDisposable _subscription;

        public QuestHandler(
            QuestPresenter presenter,
            ISubscriber<M_QuestShown> shownSubscriber)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            
            _shownSubscriber = shownSubscriber ?? throw new ArgumentNullException(nameof(shownSubscriber));
        }

        public void Start()
        {
            _subscription = _shownSubscriber.Subscribe(Show);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void Show(M_QuestShown message)
        {
            _presenter.Show(message.ViewData);
        }
    }
}
