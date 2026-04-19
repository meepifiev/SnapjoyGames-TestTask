using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Messages;
using VContainer.Unity;

namespace Project.Scripts.Runtime.UI.Interaction.Items
{
    public class ItemInspectionHandler : IStartable, IDisposable
    {
        private readonly ItemInspectionPresenter _presenter;
        
        private readonly ISubscriber<M_ItemInspectionShown> _shownSubscriber;
        private readonly ISubscriber<M_ItemInspectionHidden> _hiddenSubscriber;
        
        private IDisposable _subscriptions;

        public ItemInspectionHandler(
            ItemInspectionPresenter presenter,
            ISubscriber<M_ItemInspectionShown> shownSubscriber,
            ISubscriber<M_ItemInspectionHidden> hiddenSubscriber)
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

        private void Show(M_ItemInspectionShown message)
        {
            _presenter.Show(message.Definition);
        }

        private void Hide(M_ItemInspectionHidden message)
        {
            _presenter.Hide();
        }
    }
}
