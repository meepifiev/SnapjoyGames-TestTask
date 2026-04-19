using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.ValveDoors
{
    [CreateAssetMenu(fileName = "ValveDoorSettings", menuName = "Project/Interaction/Valve Doors/Valve Door Settings")]
    public class ValveDoorSettings : ScriptableObject
    {
        [field: Header("Text")]
        [field: SerializeField] public string RotateInteractionText { get; private set; }

        [field: Header("Motion")]
        [field: SerializeField] public ValveDoorMotionSettings MotionSettings { get; private set; }
    }
}
