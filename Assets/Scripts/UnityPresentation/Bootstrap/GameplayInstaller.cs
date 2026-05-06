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

            var animalSpawnService = new AnimalSpawnService(worldContext.GameplayWorld);
            IRandomService randomService = new SystemRandomService();

            var stateFactory = new AnimalStateFactory(
                animalMovementService,
                ConfigMapper.ToAnimalMovementSettings(animalConfig),
                worldContext.GameplayWorld,
                herdService,
                () => hero.Position,
                animalSettings,
                randomService);

            var collectionService = new AnimalCollectionService(
                herdService,
                animalSettings,
                stateFactory);

            var animalDeliveryService = new AnimalDeliveryService(
                herdService,
                scoreService,
                worldContext.GameplayWorld);

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

            IDisposableService runtimeBindings = new GameplayRuntimeBindings(
                animalSpawnService,
                animalDeliveryService);

            return new GameplayContext(
                hero,
                scoreService,
                gameplayUpdateService,
                animalSpawnService,
                animalDeliveryService,
                spawnTimerService,
                heroInputService,
                runtimeBindings);
        }

        private sealed class GameplayRuntimeBindings : IDisposableService
        {
            private readonly AnimalSpawnService _animalSpawnService;
            private readonly AnimalDeliveryService _animalDeliveryService;

            public GameplayRuntimeBindings(
                AnimalSpawnService animalSpawnService,
                AnimalDeliveryService animalDeliveryService)
            {
                _animalSpawnService = animalSpawnService;
                _animalDeliveryService = animalDeliveryService;

                _animalDeliveryService.Delivered += OnAnimalDelivered;
            }

            public void Dispose()
            {
                _animalDeliveryService.Delivered -= OnAnimalDelivered;
            }

            private void OnAnimalDelivered(AnimalModel animal)
            {
                _animalSpawnService.Despawn(animal);
            }
        }
    }
}
