using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Project/Player/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: Header("Movement")]
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public float Gravity { get; private set; } = -25f;
        [field: SerializeField] public float GroundedVerticalVelocity { get; private set; } = -2f;

        [field: Header("Look")]
        [field: SerializeField, Min(0)] public float LookSensitivity { get; private set; } = 2f;
        [field: SerializeField] public float MinPitch { get; private set; } = -80f;
        [field: SerializeField] public float MaxPitch { get; private set; } = 80f;

        [field: Header("Cursor")]
        [field: SerializeField] public bool DefaultLockCursor { get; private set; } = true;
    }
}
