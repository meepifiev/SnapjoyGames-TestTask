using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    [Serializable]
    public class DialogueTransition
    {
        [SerializeField] private bool _endsDialogue;
        [SerializeField, Min(0)] private int _nextNodeIndex;

        public bool EndsDialogue => _endsDialogue;
        public int NextNodeIndex => _nextNodeIndex;
    }
}
