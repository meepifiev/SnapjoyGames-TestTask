using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Switches
{
    [CreateAssetMenu(fileName = "LightSwitchSettings", menuName = "Project/Interaction/Switches/Light Switch Settings")]
    public class LightSwitchSettings : ScriptableObject
    {
        [field: Header("Text")]
        [field: SerializeField] public string TurnOnInteractionText { get; private set; }
        [field: SerializeField] public string TurnOffInteractionText { get; private set; }
    }
}
