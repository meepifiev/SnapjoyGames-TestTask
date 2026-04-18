using Project.Scripts.Runtime.Features.Interaction.Common;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class PickupItem : MonoBehaviour, IInteractionTarget
    {
        [SerializeField] private ItemDefinition _definition;

        public ItemDefinition Definition => _definition;

        public InteractionHint GetHint(InteractionActor actor)
        {
            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }

        public void Press(InteractionActor actor)
        {
            Debug.Log($"{_definition.DisplayName}: {_definition.Description}", this);
        }

        public void Hold(InteractionActor actor, float deltaTime)
        {
        }

        public void Release(InteractionActor actor)
        {
        }
    }
}
