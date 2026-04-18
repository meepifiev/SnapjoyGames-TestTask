using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class PickupItem : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private ItemDefinition _definition;

        private bool _isInspecting;

        public ItemDefinition Definition => _definition;

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _definition != null &&
                   string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }

        public void Press(InteractionActor actor)
        {
            if (_isInspecting)
            {
                StopInspection(actor);
                return;
            }

            StartInspection(actor);
        }

        public void Hold(InteractionActor actor, float deltaTime)
        {
        }

        public void Release(InteractionActor actor)
        {
        }

        private void StartInspection(InteractionActor actor)
        {
            _isInspecting = true;
            actor.ViewLock.LockMovement();
            actor.ViewLock.LockLook();
            actor.ItemInspectionOutput.Show(_definition);
        }

        private void StopInspection(InteractionActor actor)
        {
            _isInspecting = false;
            actor.ItemInspectionOutput.Hide();
            actor.ViewLock.UnlockLook();
            actor.ViewLock.UnlockMovement();
        }
    }
}
