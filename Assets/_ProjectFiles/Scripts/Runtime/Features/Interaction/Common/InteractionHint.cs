namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public readonly struct InteractionHint
    {
        public InteractionHint(string actionText)
        {
            ActionText = actionText;
        }

        public string ActionText { get; }
    }
}
