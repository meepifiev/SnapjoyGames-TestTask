using System;
using Project.Scripts.Runtime.Features.Interaction.Quests;

namespace Project.Scripts.Runtime.UI.Interaction.Quests
{
    public class QuestPresenter : IQuestOutput
    {
        private readonly IQuestView _view;

        public QuestPresenter(IQuestView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Show(QuestViewData viewData)
        {
            _view.Show(viewData);
        }
    }
}
