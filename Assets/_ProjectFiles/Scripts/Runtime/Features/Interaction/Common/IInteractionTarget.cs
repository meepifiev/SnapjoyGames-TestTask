namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public interface IInteractionTarget
    {
        InteractionHint GetHint(InteractionActor actor);
        bool CanInteract(InteractionActor actor);
        void Press(InteractionActor actor);
        void Hold(InteractionActor actor, float deltaTime);
        void Release(InteractionActor actor);
    }
}
