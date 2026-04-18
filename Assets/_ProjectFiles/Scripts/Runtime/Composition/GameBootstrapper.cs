using System;
using Project.Scripts.Runtime.Features.Player;
using VContainer.Unity;

namespace Project.Scripts.Runtime.Composition
{
    public class GameBootstrapper : IStartable
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly PlayerSpawnPoint _playerSpawnPoint;

        public GameBootstrapper(PlayerSpawner playerSpawner, PlayerSpawnPoint playerSpawnPoint)
        {
            _playerSpawner = playerSpawner ?? throw new ArgumentNullException(nameof(playerSpawner));
            _playerSpawnPoint = playerSpawnPoint ?? throw new ArgumentNullException(nameof(playerSpawnPoint));
        }

        public void Start()
        {
            _playerSpawner.Spawn(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation);
        }
    }
}
