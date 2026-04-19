using MessagePipe;
using Project.Scripts.Runtime.Core.Factory;
using Project.Scripts.Runtime.Core.Input;
using Project.Scripts.Runtime.Core.Time;
using Project.Scripts.Runtime.Features.Interaction.Common;
using Project.Scripts.Runtime.Features.Interaction.Quests;
using Project.Scripts.Runtime.Features.Messages;
using Project.Scripts.Runtime.Features.Player;
using Project.Scripts.Runtime.Infrastructure.Controls;
using Project.Scripts.Runtime.Infrastructure.Factory;
using Project.Scripts.Runtime.Infrastructure.Time;
using Project.Scripts.Runtime.UI.Interaction;
using Project.Scripts.Runtime.UI.Interaction.Dialogs;
using Project.Scripts.Runtime.UI.Interaction.Items;
using Project.Scripts.Runtime.UI.Interaction.Quests;
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

        [Header("UI")]
        [SerializeField] private InteractionHintSettings _interactionHintSettings;
        [SerializeField] private InteractionHintView _interactionHintView;
        [SerializeField] private ItemInspectionView _itemInspectionView;
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private QuestView _questView;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureMessagePipe(builder);
            ConfigureInput(builder);
            ConfigureTimeProvider(builder);
            ConfigureFactories(builder);
            ConfigureInteraction(builder);
            ConfigurePlayer(builder);
            ConfigureUI(builder);
            ConfigureEntryPoints(builder);
        }

        private void ConfigureInput(IContainerBuilder builder)
        {
            builder.Register<IInputReader, InputReader>(Lifetime.Scoped);
        }

        private void ConfigureTimeProvider(IContainerBuilder builder)
        {
            builder.Register<ITimeProvider, TimeProvider>(Lifetime.Scoped);
        }

        private void ConfigureFactories(IContainerBuilder builder)
        {
            builder.Register<IObjectFactory, ObjectFactory>(Lifetime.Scoped);
        }

        private void ConfigureMessagePipe(IContainerBuilder builder)
        {
            MessagePipeOptions options = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<M_ItemInspectionShown>(options);
            builder.RegisterMessageBroker<M_ItemInspectionHidden>(options);
            
            builder.RegisterMessageBroker<M_InteractionHintShown>(options);
            builder.RegisterMessageBroker<M_InteractionHintHidden>(options);
            
            builder.RegisterMessageBroker<M_DialogueShown>(options);
            builder.RegisterMessageBroker<M_DialogueHidden>(options);
            
            builder.RegisterMessageBroker<M_QuestShown>(options);
            
            builder.RegisterMessageBroker<M_PlayerMovementLockChanged>(options);
            builder.RegisterMessageBroker<M_PlayerLookLockChanged>(options);
            builder.RegisterMessageBroker<M_PlayerInteractionLockChanged>(options);
            builder.RegisterMessageBroker<M_PlayerCursorVisibilityChanged>(options);
        }

        private void ConfigureInteraction(IContainerBuilder builder)
        {
            builder.Register<InteractionPipe>(Lifetime.Scoped);
            builder.Register<IQuestItemSelector, SceneQuestItemSelector>(Lifetime.Scoped);
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerPrefabSettings);
            builder.RegisterComponent(_playerSpawnPoint);
            builder.Register<PlayerSpawner>(Lifetime.Scoped);
        }

        private void ConfigureUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(_interactionHintSettings);

            builder.RegisterComponent(_interactionHintView).As<IInteractionHintView>();
            builder.Register<InteractionHintPresenter>(Lifetime.Scoped);

            builder.RegisterComponent(_itemInspectionView).As<IItemInspectionView>();
            builder.Register<ItemInspectionPresenter>(Lifetime.Scoped);

            builder.RegisterComponent(_dialogueView).As<IDialogueView>();
            builder.Register<DialoguePresenter>(Lifetime.Scoped);

            builder.RegisterComponent(_questView).As<IQuestView>();
            builder.Register<QuestPresenter>(Lifetime.Scoped);
        }

        private void ConfigureEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InteractionHintHandler>(Lifetime.Scoped);
            builder.RegisterEntryPoint<ItemInspectionHandler>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<DialogueHandler>(Lifetime.Scoped);
            builder.RegisterEntryPoint<QuestHandler>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameBootstrapper>();
        }
    }
}
