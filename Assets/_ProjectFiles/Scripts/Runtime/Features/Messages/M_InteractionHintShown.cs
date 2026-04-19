using Project.Scripts.Runtime.Features.Interaction.Common;

namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_InteractionHintShown
    {
        public M_InteractionHintShown(InteractionHint hint)
        {
            Hint = hint;
        }

        public InteractionHint Hint { get; }
    }
}
