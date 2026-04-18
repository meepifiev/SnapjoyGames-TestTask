using Project.Scripts.Runtime.Core.Factory;
using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Player;
using Project.Scripts.Runtime.Infrastructure.Controls;
using Project.Scripts.Runtime.Infrastructure.Factory;
using Project.Scripts.Runtime.Infrastructure.Time;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Runtime.Composition
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Player")]
        [SerializeField] private PlayerPrefabSettings _playerPrefabSettings;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureInfrastructure(builder);
            ConfigureFactories(builder);
            ConfigurePlayer(builder);
            ConfigureEntryPoints(builder);
        }

        private void ConfigureInfrastructure(IContainerBuilder builder)
        {
            builder.Register<IInputReader, InputReader>(Lifetime.Scoped);
            builder.Register<ITimeProvider, TimeProvider>(Lifetime.Scoped);
        }

        private void ConfigureFactories(IContainerBuilder builder)
        {
            builder.Register<IObjectFactory, ObjectFactory>(Lifetime.Scoped);
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerPrefabSettings);
            builder.RegisterComponent(_playerSpawnPoint);
            builder.Register<PlayerSpawner>(Lifetime.Scoped);
        }

        private void ConfigureEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameBootstrapper>();
        }
    }
}
