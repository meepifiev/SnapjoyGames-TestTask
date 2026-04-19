using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public class Npc : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private NpcDefinition _definition;

        private InteractionActor _actor;
        private DialogueSession _session;
        
        private bool _isActive;

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _isActive == false &&
                   _definition.Dialogue.HasNode(_definition.Dialogue.StartNodeIndex) &&
                   string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }

        public void Press(InteractionActor actor)
        {
            if (CanInteract(actor) == false)
                return;

            _actor = actor;
            _session = new DialogueSession(_definition);
            _isActive = true;

            _actor.ViewLock.LockMovement();
            _actor.ViewLock.LockLook();
            _actor.ViewLock.LockInteraction();
            _actor.Cursor.RequestVisible();

            ShowCurrentNode();
        }

        public void Hold(InteractionActor actor, float deltaTime) { }

        public void Release(InteractionActor actor) { }

        private void ShowCurrentNode()
        {
            _actor.DialogueOutput.Show(_session.CreateViewData(Finish, ShowCurrentNode));
        }

        private void Finish()
        {
            _actor.DialogueOutput.Hide();
            _actor.Cursor.ReleaseVisible();
            _actor.ViewLock.UnlockInteraction();
            _actor.ViewLock.UnlockLook();
            _actor.ViewLock.UnlockMovement();

            _session = null;
            _actor = null;
            _isActive = false;
        }
    }
}
