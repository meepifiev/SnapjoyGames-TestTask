using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [CreateAssetMenu(fileName = "NoteInspectionAnimationSettings", menuName = "Project/Interaction/Items/Note Inspection Animation Settings")]
    public class NoteInspectionAnimationSettings : ScriptableObject
    {
        [field: Header("Pages")]
        [field: SerializeField] public Vector3 LeftPageOpenLocalEulerAngles { get; private set; }
        [field: SerializeField] public Vector3 RightPageOpenLocalEulerAngles { get; private set; }

        [field: Header("Animation")]
        [field: SerializeField, Min(0)] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}
