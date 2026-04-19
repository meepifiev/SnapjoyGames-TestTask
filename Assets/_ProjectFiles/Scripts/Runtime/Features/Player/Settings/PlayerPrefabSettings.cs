using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    [CreateAssetMenu(fileName = "PlayerPrefabSettings", menuName = "Project/Player/Player Prefab Settings")]
    public class PlayerPrefabSettings : ScriptableObject
    {
        [field: SerializeField] public PlayerRoot Prefab { get; private set; }
    }
}
