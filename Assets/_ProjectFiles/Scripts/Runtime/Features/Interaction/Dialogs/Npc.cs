using System;
using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;
using VContainer;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    public class Npc : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private NpcDefinition _definition;

        private InteractionActor _actor;
        private InteractionPipe _pipe;
        private DialogueSession _session;
        
        private bool _isActive;

        [Inject]
        private void Construct(InteractionPipe pipe)
        {
            _pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
        }

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

            _pipe.LockMovement();
            _pipe.LockLook();
            _pipe.LockInteraction();
            _pipe.RequestCursorVisible();

            ShowCurrentNode();
        }

        public void Hold(InteractionActor actor, float deltaTime) { }

        public void Release(InteractionActor actor) { }

        private void ShowCurrentNode()
        {
            _pipe.ShowDialogue(_session.CreateViewData(Finish, ShowCurrentNode));
        }

        private void Finish()
        {
            _pipe.HideDialogue();
            _pipe.ReleaseCursorVisible();
            _pipe.UnlockInteraction();
            _pipe.UnlockLook();
            _pipe.UnlockMovement();

            _session = null;
            _actor = null;
            _isActive = false;
        }
    }
}
