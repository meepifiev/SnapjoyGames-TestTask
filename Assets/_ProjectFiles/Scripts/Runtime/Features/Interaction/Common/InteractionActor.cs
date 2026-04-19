using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Dialogs;
using Project.Scripts.Runtime.Features.Interaction.Items;
using Project.Scripts.Runtime.Features.Interaction.Quests;
using Project.Scripts.Runtime.Features.Player;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Common
{
    public class InteractionActor
    {
        public InteractionActor(
            Transform viewPoint,
            Transform itemInspectionHolder,
            Transform heldItemHolder,
            HeldItemSlot heldItemSlot,
            PlayerViewLock viewLock,
            PlayerCursor cursor,
            IInputReader inputReader,
            ITimeProvider timeProvider,
            IDialogueOutput dialogueOutput,
            IQuestOutput questOutput,
            IItemInspectionOutput itemInspectionOutput)
        {
            ViewPoint = viewPoint;
            ItemInspectionHolder = itemInspectionHolder;
            HeldItemHolder = heldItemHolder;
            HeldItemSlot = heldItemSlot;
            ViewLock = viewLock;
            Cursor = cursor;
            InputReader = inputReader;
            TimeProvider = timeProvider;
            DialogueOutput = dialogueOutput;
            QuestOutput = questOutput;
            ItemInspectionOutput = itemInspectionOutput;
        }

        public Transform ViewPoint { get; }
        public Transform ItemInspectionHolder { get; }
        public Transform HeldItemHolder { get; }
        public HeldItemSlot HeldItemSlot { get; }
        public PlayerViewLock ViewLock { get; }
        public PlayerCursor Cursor { get; }
        public IInputReader InputReader { get; }
        public ITimeProvider TimeProvider { get; }
        public IDialogueOutput DialogueOutput { get; }
        public IQuestOutput QuestOutput { get; }
        public IItemInspectionOutput ItemInspectionOutput { get; }
    }
}
