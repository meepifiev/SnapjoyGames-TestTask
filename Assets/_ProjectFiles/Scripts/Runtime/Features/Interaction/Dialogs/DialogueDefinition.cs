using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Dialogs
{
    [CreateAssetMenu(fileName = "DialogueDefinition", menuName = "Project/Interaction/Dialogs/Dialogue Definition")]
    public class DialogueDefinition : ScriptableObject
    {
        [SerializeField, Min(0)] private int _startNodeIndex;
        [SerializeField] private DialogueNodeDefinition[] _nodes;

        public int StartNodeIndex => _startNodeIndex;

        public bool HasNode(int nodeIndex)
        {
            return nodeIndex >= 0 &&
                   nodeIndex < _nodes.Length &&
                   _nodes[nodeIndex] != null;
        }

        public DialogueNodeDefinition GetNode(int nodeIndex)
        {
            return _nodes[nodeIndex];
        }
    }
}
