using UnityEngine;

namespace Project.Scripts.Runtime.UI.Interaction
{
    [CreateAssetMenu(fileName = "InteractionHintSettings", menuName = "Project/Interaction/UI/Interaction Hint Settings")]
    public class InteractionHintSettings : ScriptableObject
    {
        [field: SerializeField] public string InteractionKeyLabel { get; private set; }
        [field: SerializeField] public string Separator { get; private set; }
    }
}
