using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}
