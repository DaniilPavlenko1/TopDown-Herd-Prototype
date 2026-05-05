using Application.Animals;
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
using UnityPresentation.Bindings;
using UnityPresentation.Input;
using UnityPresentation.Pooling;
using UnityPresentation.World;

namespace UnityPresentation.Bootstrap
{
    public sealed class SceneCompositionRoot : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField] private SceneReferences sceneReferences;

        [Header("Configs")]
        [SerializeField] private HeroConfig heroConfig;
        [SerializeField] private AnimalConfig animalConfig;
        [SerializeField] private HerdConfig herdConfig;
        [SerializeField] private SpawnerConfig spawnerConfig;

        [Header("World Layout")]
        [SerializeField] private float visibleHeight = 10f;
        [SerializeField, Range(0.1f, 0.4f)] private float yardWidthPercent = 0.18f;
        [SerializeField, Range(0.2f, 0.9f)] private float yardHeightPercent = 0.45f;
        [SerializeField] private float horizontalPadding = 1f;
        [SerializeField] private float verticalPadding = 1f;
        [SerializeField] private float gapBetweenSpawnAndYard = 1f;

        private GameplayLifetime _lifetime;
        private GameplayUpdateService _gameplayUpdateService;
        private HeroInputService _heroInputService;

        private HeroViewBinder _heroViewBinder;
        private AnimalViewRegistry _animalViewRegistry;

        private AnimalSpawnService _animalSpawnService;
        private AnimalDeliveryService _animalDeliveryService;

        private GameplayWorld _gameplayWorld;

        private void Awake()
        {
            ValidateReferences();

            _lifetime = new GameplayLifetime();

            BuildWorld();
            BuildGameplay();

            _lifetime.Initialize();
        }

        private void Update()
        {
            _lifetime.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _lifetime?.Dispose();

            if (_animalSpawnService != null)
                _animalSpawnService.Spawned -= OnAnimalSpawned;

            if (_animalDeliveryService != null)
                _animalDeliveryService.Delivered -= OnAnimalDelivered;

            sceneReferences.ScoreView.Unbind();
            _animalViewRegistry?.Clear();
        }

        private void BuildWorld()
        {
            var layoutSettings = new WorldLayoutSettings(
                visibleHeight,
                sceneReferences.CameraView.AspectRatio,
                yardWidthPercent,
                yardHeightPercent,
                horizontalPadding,
                verticalPadding,
                gapBetweenSpawnAndYard);

            var layoutService = new WorldLayoutService();
            WorldLayout layout = layoutService.Calculate(layoutSettings);

            _gameplayWorld = new GameplayWorld(
                layout.WorldBounds,
                layout.SpawnBounds,
                layout.YardBounds);

            var layoutApplier = new WorldLayoutApplier(
                sceneReferences.CameraView,
                sceneReferences.GroundView,
                sceneReferences.YardView,
                sceneReferences.SpawnAreaView);

            layoutApplier.Apply(layout);
        }

        private void BuildGameplay()
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

            IPlayerInput playerInput = new MouseInputAdapter(sceneReferences.MainCamera);
            _heroInputService = new HeroInputService(hero, playerInput);

            _animalSpawnService = new AnimalSpawnService(_gameplayWorld);

            var stateFactory = new AnimalStateFactory(
                animalMovementService,
                ConfigMapper.ToAnimalMovementSettings(animalConfig),
                _gameplayWorld,
                herdService,
                () => hero.Position,
                animalSettings);

            var collectionService = new AnimalCollectionService(
                herdService,
                animalSettings,
                stateFactory);

            _animalDeliveryService = new AnimalDeliveryService(
                herdService,
                scoreService,
                _gameplayWorld);

            _gameplayUpdateService = new GameplayUpdateService(
                playerInput,
                hero,
                heroMovementService,
                _animalSpawnService,
                collectionService,
                _animalDeliveryService);

            var animalViewPool = new AnimalViewPool(
                sceneReferences.AnimalPrefab,
                sceneReferences.AnimalsContainer);

            _animalViewRegistry = new AnimalViewRegistry(animalViewPool);
            _heroViewBinder = new HeroViewBinder(hero, sceneReferences.HeroView);
            var spawnTimerService = new AnimalSpawnTimerService(
                _animalSpawnService,
                stateFactory,
                spawnerConfig.InitialSpawnCount,
                spawnerConfig.MaxAliveAnimals,
                spawnerConfig.SpawnIntervalMin,
                spawnerConfig.SpawnIntervalMax);
            var viewTickService = new ViewTickService(
                _heroViewBinder,
                _animalViewRegistry);

            sceneReferences.ScoreView.Bind(scoreService);

            _animalSpawnService.Spawned += OnAnimalSpawned;
            _animalDeliveryService.Delivered += OnAnimalDelivered;

            _lifetime.AddInitializable(spawnTimerService);
            _lifetime.AddTickable(_gameplayUpdateService);
            _lifetime.AddTickable(spawnTimerService);
            _lifetime.AddTickable(viewTickService);

            _lifetime.AddDisposable(_heroInputService);
            _lifetime.AddDisposable(viewTickService);
        }

        private void OnAnimalSpawned(AnimalModel animal)
        {
            _animalViewRegistry.Add(animal);
        }

        private void OnAnimalDelivered(AnimalModel animal)
        {
            _animalViewRegistry.Remove(animal);
            _animalSpawnService.Despawn(animal);
        }

        private void ValidateReferences()
        {
            Debug.Assert(sceneReferences != null, "SceneReferences is not assigned.");
            Debug.Assert(heroConfig != null, "HeroConfig is not assigned.");
            Debug.Assert(animalConfig != null, "AnimalConfig is not assigned.");
            Debug.Assert(herdConfig != null, "HerdConfig is not assigned.");
            Debug.Assert(spawnerConfig != null, "SpawnerConfig is not assigned.");
        }
    }
}
