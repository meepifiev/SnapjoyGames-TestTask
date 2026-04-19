using System;
using Project.Scripts.Runtime.Core.Factory;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Player
{
    public class PlayerSpawner
    {
        private readonly IObjectFactory _objectFactory;
        private readonly PlayerPrefabSettings _prefabSettings;

        public PlayerSpawner(IObjectFactory objectFactory, PlayerPrefabSettings prefabSettings)
        {
            _objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory));
            _prefabSettings = prefabSettings ?? throw new ArgumentNullException(nameof(prefabSettings));;
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            _objectFactory.Create(_prefabSettings.Prefab, position, rotation);
        }
    }
}
