using Application.Animals;
using Application.Common;
using Application.Gameplay;
using Application.Input;
using Application.World;
using Configs;
using Domain.Animals;
using Domain.Common;
using Domain.Herd;
using Domain.Hero;
using Domain.Movement;
using Domain.Score;
using UnityEngine;
using UnityPresentation.Input;

namespace UnityPresentation.Bootstrap
{
    public sealed class GameplayInstaller
    {
        public GameplayContext Install(
            WorldContext worldContext,
            HeroConfig heroConfig,
            AnimalConfig animalConfig,
            HerdConfig herdConfig,
            SpawnerConfig spawnerConfig,
            Camera mainCamera)
        {
            var hero = new HeroModel(GameVector2.Zero);
            IEventBus eventBus = new EventBus();

            var animalSettings = ConfigMapper.ToAnimalSettings(animalConfig);
            var herdSettings = ConfigMapper.ToHerdSettings(herdConfig);

            var movementService = new MovementService();
            var heroMovementService = new HeroMovementService(
                movementService,
                ConfigMapper.ToHeroMovementSettings(heroConfig));

            var animalMovementService = new AnimalMovementService(movementService);

            IHerdService herdService = new HerdService(herdSettings);
            IScoreService scoreService = new ScoreService();

            IPlayerInput playerInput = new MouseInputAdapter(mainCamera);
            var heroInputService = new HeroInputService(hero, playerInput);

            IRandomService spawnRandomService = new SystemRandomService();
            var animalSpawnService = new AnimalSpawnService(
                worldContext.GameplayWorld,
                eventBus,
                spawnRandomService);
            IRandomService patrolRandomService = new SystemRandomService();

            var stateFactory = new AnimalStateFactory(
                animalMovementService,
                ConfigMapper.ToAnimalMovementSettings(animalConfig),
                worldContext.GameplayWorld,
                herdService,
                () => hero.Position,
                animalSettings,
                patrolRandomService);

            var collectionService = new AnimalCollectionService(
                herdService,
                animalSettings,
                stateFactory);

            var animalDeliveryService = new AnimalDeliveryService(
                herdService,
                worldContext.GameplayWorld,
                eventBus);

            var gameplayUpdateService = new GameplayUpdateService(
                playerInput,
                hero,
                heroMovementService,
                animalSpawnService,
                collectionService,
                animalDeliveryService);

            var spawnTimerService = new AnimalSpawnTimerService(
                animalSpawnService,
                stateFactory,
                spawnerConfig.InitialSpawnCount,
                spawnerConfig.MaxAliveAnimals,
                spawnerConfig.SpawnIntervalMin,
                spawnerConfig.SpawnIntervalMax);

            IDisposableService runtimeBindings = new CompositeDisposableService(
                new AnimalDeliveredScoreHandler(eventBus, scoreService),
                new AnimalDeliveredDespawnHandler(eventBus, animalSpawnService));

            return new GameplayContext(
                hero,
                scoreService,
                eventBus,
                gameplayUpdateService,
                spawnTimerService,
                heroInputService,
                runtimeBindings);
        }
    }
}
