using System;
using MessagePipe;
using Project.Scripts.Runtime.Features.Interaction.Dialogs;
using Project.Scripts.Runtime.Features.Interaction.Items;
using Project.Scripts.Runtime.Features.Messages;
using Project.Scripts.Runtime.Features.Interaction.Quests;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public class InteractionPipe
    {
        private readonly IPublisher<M_ItemInspectionShown> _itemInspectionShownPublisher;
        private readonly IPublisher<M_ItemInspectionHidden> _itemInspectionHiddenPublisher;
        
        private readonly IPublisher<M_InteractionHintShown> _interactionHintShownPublisher;
        private readonly IPublisher<M_InteractionHintHidden> _interactionHintHiddenPublisher;
        
        private readonly IPublisher<M_DialogueShown> _dialogueShownPublisher;
        private readonly IPublisher<M_DialogueHidden> _dialogueHiddenPublisher;
        private readonly IPublisher<M_QuestShown> _questShownPublisher;
        
        private readonly IPublisher<M_PlayerMovementLockChanged> _movementLockPublisher;
        private readonly IPublisher<M_PlayerLookLockChanged> _lookLockPublisher;
        private readonly IPublisher<M_PlayerInteractionLockChanged> _interactionLockPublisher;
        private readonly IPublisher<M_PlayerCursorVisibilityChanged> _cursorVisibilityPublisher;

        public InteractionPipe(
            IPublisher<M_ItemInspectionShown> itemInspectionShownPublisher,
            IPublisher<M_ItemInspectionHidden> itemInspectionHiddenPublisher,
            IPublisher<M_InteractionHintShown> interactionHintShownPublisher,
            IPublisher<M_InteractionHintHidden> interactionHintHiddenPublisher,
            IPublisher<M_DialogueShown> dialogueShownPublisher,
            IPublisher<M_DialogueHidden> dialogueHiddenPublisher,
            IPublisher<M_QuestShown> questShownPublisher,
            IPublisher<M_PlayerMovementLockChanged> movementLockPublisher,
            IPublisher<M_PlayerLookLockChanged> lookLockPublisher,
            IPublisher<M_PlayerInteractionLockChanged> interactionLockPublisher,
            IPublisher<M_PlayerCursorVisibilityChanged> cursorVisibilityPublisher)
        {
            _itemInspectionShownPublisher = itemInspectionShownPublisher ?? throw new ArgumentNullException(nameof(itemInspectionShownPublisher));
            _itemInspectionHiddenPublisher = itemInspectionHiddenPublisher ?? throw new ArgumentNullException(nameof(itemInspectionHiddenPublisher));
            
            _interactionHintShownPublisher = interactionHintShownPublisher ?? throw new ArgumentNullException(nameof(interactionHintShownPublisher));
            _interactionHintHiddenPublisher = interactionHintHiddenPublisher ?? throw new ArgumentNullException(nameof(interactionHintHiddenPublisher));
            
            _dialogueShownPublisher = dialogueShownPublisher ?? throw new ArgumentNullException(nameof(dialogueShownPublisher));
            _dialogueHiddenPublisher = dialogueHiddenPublisher ?? throw new ArgumentNullException(nameof(dialogueHiddenPublisher));
            _questShownPublisher = questShownPublisher ?? throw new ArgumentNullException(nameof(questShownPublisher));
            
            _movementLockPublisher = movementLockPublisher ?? throw new ArgumentNullException(nameof(movementLockPublisher));
            _lookLockPublisher = lookLockPublisher ?? throw new ArgumentNullException(nameof(lookLockPublisher));
            _interactionLockPublisher = interactionLockPublisher ?? throw new ArgumentNullException(nameof(interactionLockPublisher));
            _cursorVisibilityPublisher = cursorVisibilityPublisher ?? throw new ArgumentNullException(nameof(cursorVisibilityPublisher));
        }

        public void ShowItemInspection(ItemDefinition definition)
        {
            _itemInspectionShownPublisher.Publish(new M_ItemInspectionShown(definition));
        }

        public void HideItemInspection()
        {
            _itemInspectionHiddenPublisher.Publish(new M_ItemInspectionHidden());
        }

        public void ShowInteractionHint(InteractionHint hint)
        {
            _interactionHintShownPublisher.Publish(new M_InteractionHintShown(hint));
        }

        public void HideInteractionHint()
        {
            _interactionHintHiddenPublisher.Publish(new M_InteractionHintHidden());
        }

        public void ShowDialogue(DialogueViewData viewData)
        {
            _dialogueShownPublisher.Publish(new M_DialogueShown(viewData));
        }

        public void HideDialogue()
        {
            _dialogueHiddenPublisher.Publish(new M_DialogueHidden());
        }

        public void ShowQuest(QuestViewData viewData)
        {
            _questShownPublisher.Publish(new M_QuestShown(viewData));
        }

        public void LockMovement()
        {
            _movementLockPublisher.Publish(new M_PlayerMovementLockChanged(true));
        }

        public void UnlockMovement()
        {
            _movementLockPublisher.Publish(new M_PlayerMovementLockChanged(false));
        }

        public void LockLook()
        {
            _lookLockPublisher.Publish(new M_PlayerLookLockChanged(true));
        }

        public void UnlockLook()
        {
            _lookLockPublisher.Publish(new M_PlayerLookLockChanged(false));
        }

        public void LockInteraction()
        {
            _interactionLockPublisher.Publish(new M_PlayerInteractionLockChanged(true));
        }

        public void UnlockInteraction()
        {
            _interactionLockPublisher.Publish(new M_PlayerInteractionLockChanged(false));
        }

        public void RequestCursorVisible()
        {
            _cursorVisibilityPublisher.Publish(new M_PlayerCursorVisibilityChanged(true));
        }

        public void ReleaseCursorVisible()
        {
            _cursorVisibilityPublisher.Publish(new M_PlayerCursorVisibilityChanged(false));
        }
    }
}
