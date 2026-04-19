namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    public readonly struct QuestViewData
    {
        public QuestViewData(string description, bool isCompleted)
        {
            Description = description;
            IsCompleted = isCompleted;
        }

        public string Description { get; }
        public bool IsCompleted { get; }
    }
}
