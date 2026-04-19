using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Dialogs;
using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;
using VContainer;

namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    public class QuestNpc : MonoBehaviour, IInteractionTarget
    {
        private const int FirstLineIndex = 0;
        private const int NextLineStep = 1;

        [SerializeField] private QuestNpcDefinition _definition;

        private readonly List<DialogueChoiceViewData> _choices = new();

        private InteractionActor _actor;
        
        private PickupItem _requiredItem;
        
        private string _questDescription;
        private int _lineIndex;
        private bool _isDialogueActive;
        private bool _isQuestActive;
        private bool _isQuestCompleted;
        
        private InteractionPipe _pipe;
        private IQuestItemSelector _itemSelector;

        [Inject]
        private void Construct(InteractionPipe pipe, IQuestItemSelector itemSelector)
        {
            _pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
            _itemSelector = itemSelector ?? throw new ArgumentNullException(nameof(itemSelector));
        }

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

            _pipe.LockMovement();
            _pipe.LockLook();
            _pipe.LockInteraction();
            _pipe.RequestCursorVisible();

            ShowQuestLine();
        }

        private void ShowQuestLine()
        {
            if (IsLastLine() && _requiredItem == null)
                _requiredItem = _itemSelector.Select(_definition.ExcludedItemTypes);

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
            
            _pipe.ShowDialogue(new DialogueViewData(_definition.DisplayName, CreateCurrentLine(), _choices));
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
            _pipe.ShowQuest(new QuestViewData(_questDescription, false));

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
            _pipe.ShowQuest(new QuestViewData(_questDescription, true));

            StartSingleLineDialogue(actor, _definition.CompleteLine);
        }

        private void StartSingleLineDialogue(InteractionActor actor, string line)
        {
            _actor = actor;
            _isDialogueActive = true;

            _pipe.LockMovement();
            _pipe.LockLook();
            _pipe.LockInteraction();
            _pipe.RequestCursorVisible();

            ShowSingleLine(line, FinishDialogue);
        }

        private void ShowSingleLine(string line, Action close)
        {
            _choices.Clear();
            _choices.Add(new DialogueChoiceViewData(_definition.CloseChoiceText, close));
            _pipe.ShowDialogue(new DialogueViewData(_definition.DisplayName, line, _choices));
        }

        private void FinishDialogue()
        {
            _pipe.HideDialogue();
            _pipe.ReleaseCursorVisible();
            _pipe.UnlockInteraction();
            _pipe.UnlockLook();
            _pipe.UnlockMovement();

            _actor = null;
            _isDialogueActive = false;
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
