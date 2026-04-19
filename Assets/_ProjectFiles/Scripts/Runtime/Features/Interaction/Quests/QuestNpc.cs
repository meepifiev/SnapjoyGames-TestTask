using System.Collections.Generic;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Dialogs;
using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    public class QuestNpc : MonoBehaviour, IInteractionTarget
    {
        private const int FirstLineIndex = 0;
        private const int NextLineStep = 1;

        [SerializeField] private QuestNpcDefinition _definition;

        private readonly List<DialogueChoiceViewData> _choices = new();
        private readonly List<PickupItem> _candidateItems = new();

        private InteractionActor _actor;
        private PickupItem _requiredItem;
        private string _questDescription;
        private int _lineIndex;
        private bool _isDialogueActive;
        private bool _isQuestActive;
        private bool _isQuestCompleted;

        public InteractionHint GetHint(InteractionActor actor)
        {
            if (_isQuestActive && HasRequiredItem(actor))
                return new InteractionHint(_definition.CompleteInteractionText);

            return new InteractionHint(_definition.InteractionText);
        }

        public bool CanInteract(InteractionActor actor)
        {
            return _isDialogueActive == false &&
                   _isQuestCompleted == false &&
                   IsConfigured() &&
                   HasAvailableInteractionText(actor);
        }

        public void Press(InteractionActor actor)
        {
            if (CanInteract(actor) == false)
                return;

            if (_isQuestActive)
            {
                TryCompleteQuest(actor);
                return;
            }

            StartDialogue(actor);
        }

        public void Hold(InteractionActor actor, float deltaTime) { }

        public void Release(InteractionActor actor) { }

        private void StartDialogue(InteractionActor actor)
        {
            _actor = actor;
            _lineIndex = FirstLineIndex;
            _isDialogueActive = true;

            _actor.ViewLock.LockMovement();
            _actor.ViewLock.LockLook();
            _actor.ViewLock.LockInteraction();
            _actor.Cursor.RequestVisible();

            ShowQuestLine();
        }

        private void ShowQuestLine()
        {
            if (IsLastLine() && _requiredItem == null)
                _requiredItem = SelectRandomRequiredItem();

            if (IsLastLine() && _requiredItem == null)
            {
                ShowSingleLine(_definition.NoItemLine, FinishDialogue);
                return;
            }

            string choiceText = IsLastLine()
                ? _definition.AcceptQuestChoiceText
                : _definition.ContinueChoiceText;

            _choices.Clear();
            
            _choices.Add(new DialogueChoiceViewData(choiceText, SelectQuestLine));
            
            _actor.DialogueOutput.Show(new DialogueViewData(_definition.DisplayName, CreateCurrentLine(), _choices));
        }

        private void SelectQuestLine()
        {
            if (IsLastLine())
            {
                AcceptQuest();
                return;
            }

            _lineIndex += NextLineStep;
            ShowQuestLine();
        }

        private void AcceptQuest()
        {
            if (_requiredItem == null)
            {
                ShowSingleLine(_definition.NoItemLine, FinishDialogue);
                return;
            }

            _questDescription = CreateQuestDescription(_requiredItem);
            _isQuestActive = true;
            _actor.QuestOutput.Show(new QuestViewData(_questDescription, false));

            FinishDialogue();
        }

        private void TryCompleteQuest(InteractionActor actor)
        {
            if (HasRequiredItem(actor) == false)
            {
                StartSingleLineDialogue(actor, _definition.MissingItemLine);
                return;
            }

            PickupItem item = actor.HeldItemSlot.Item;
            actor.HeldItemSlot.Clear();
            item.Consume();

            _isQuestActive = false;
            _isQuestCompleted = true;
            actor.QuestOutput.Show(new QuestViewData(_questDescription, true));

            StartSingleLineDialogue(actor, _definition.CompleteLine);
        }

        private void StartSingleLineDialogue(InteractionActor actor, string line)
        {
            _actor = actor;
            _isDialogueActive = true;

            _actor.ViewLock.LockMovement();
            _actor.ViewLock.LockLook();
            _actor.ViewLock.LockInteraction();
            _actor.Cursor.RequestVisible();

            ShowSingleLine(line, FinishDialogue);
        }

        private void ShowSingleLine(string line, System.Action close)
        {
            _choices.Clear();
            _choices.Add(new DialogueChoiceViewData(_definition.CloseChoiceText, close));
            _actor.DialogueOutput.Show(new DialogueViewData(_definition.DisplayName, line, _choices));
        }

        private void FinishDialogue()
        {
            _actor.DialogueOutput.Hide();
            _actor.Cursor.ReleaseVisible();
            _actor.ViewLock.UnlockInteraction();
            _actor.ViewLock.UnlockLook();
            _actor.ViewLock.UnlockMovement();

            _actor = null;
            _isDialogueActive = false;
        }

        private PickupItem SelectRandomRequiredItem()
        {
            _candidateItems.Clear();

            PickupItem[] items = FindObjectsByType<PickupItem>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            for (int itemIndex = 0; itemIndex < items.Length; itemIndex++)
            {
                PickupItem item = items[itemIndex];

                if (CanBeRequiredItem(item))
                    _candidateItems.Add(item);
            }

            if (_candidateItems.Count == 0)
                return null;

            return _candidateItems[Random.Range(0, _candidateItems.Count)];
        }

        private bool CanBeRequiredItem(PickupItem item)
        {
            return item != null &&
                   item.Definition != null &&
                   IsExcluded(item.Definition.Type) == false;
        }

        private bool IsExcluded(ItemType itemType)
        {
            if (_definition.ExcludedItemTypes == null)
                return false;

            foreach (ItemType type in _definition.ExcludedItemTypes)
            {
                if (type == itemType)
                    return true;
            }

            return false;
        }

        private bool HasRequiredItem(InteractionActor actor)
        {
            return _requiredItem != null &&
                   actor.HeldItemSlot.HasItem &&
                   actor.HeldItemSlot.Item == _requiredItem;
        }

        private string CreateQuestDescription(PickupItem item)
        {
            return string.Format(_definition.QuestDescriptionFormat, item.Definition.DisplayName);
        }

        private string CreateCurrentLine()
        {
            if (IsLastLine() == false || _requiredItem == null)
                return _definition.Lines[_lineIndex];

            return string.Format(_definition.Lines[_lineIndex], _requiredItem.Definition.DisplayName);
        }

        private bool IsLastLine()
        {
            return _lineIndex >= _definition.Lines.Length - NextLineStep;
        }

        private bool IsConfigured()
        {
            return _definition.Lines != null &&
                   _definition.Lines.Length > 0 &&
                   string.IsNullOrWhiteSpace(_definition.DisplayName) == false &&
                   string.IsNullOrWhiteSpace(_definition.InteractionText) == false &&
                   string.IsNullOrWhiteSpace(_definition.ContinueChoiceText) == false &&
                   string.IsNullOrWhiteSpace(_definition.AcceptQuestChoiceText) == false &&
                   string.IsNullOrWhiteSpace(_definition.NoItemLine) == false &&
                   string.IsNullOrWhiteSpace(_definition.MissingItemLine) == false &&
                   string.IsNullOrWhiteSpace(_definition.CompleteLine) == false &&
                   string.IsNullOrWhiteSpace(_definition.CloseChoiceText) == false &&
                   string.IsNullOrWhiteSpace(_definition.QuestDescriptionFormat) == false;
        }

        private bool HasAvailableInteractionText(InteractionActor actor)
        {
            if (_isQuestActive && HasRequiredItem(actor))
                return string.IsNullOrWhiteSpace(_definition.CompleteInteractionText) == false;

            return string.IsNullOrWhiteSpace(_definition.InteractionText) == false;
        }
    }
}
