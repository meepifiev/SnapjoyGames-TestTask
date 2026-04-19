using Project.Scripts.Runtime.Features.Interaction.Quests;

namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_QuestShown
    {
        public M_QuestShown(QuestViewData viewData)
        {
            ViewData = viewData;
        }

        public QuestViewData ViewData { get; }
    }
}
