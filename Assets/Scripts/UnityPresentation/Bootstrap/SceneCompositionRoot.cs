using Application.Animals;
using Application.Gameplay;
using Application.Input;
using Application.World;
using Configs;
using Domain.Animals;
using Domain.Animals.States;
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

        private GameplayUpdateService _gameplayUpdateService;
        private HeroInputService _heroInputService;

        private HeroViewBinder _heroViewBinder;
        private AnimalViewRegistry _animalViewRegistry;

        private AnimalSpawnService _animalSpawnService;
        private AnimalDeliveryService _animalDeliveryService;

        private float _spawnTimer;

        private GameplayWorld _gameplayWorld;

        private void Awake()
        {
            ValidateReferences();

            BuildWorld();
            BuildGameplay();
        }

        private void Update()
        {
            _gameplayUpdateService.Tick(Time.deltaTime);

            UpdateSpawning(Time.deltaTime);

            _heroViewBinder.Tick();
            _animalViewRegistry.Tick();
        }

        private void OnDestroy()
        {
            _heroInputService?.Dispose();

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

            var patrolState = new PatrolAnimalState(
                animalMovementService,
                ConfigMapper.ToAnimalMovementSettings(animalConfig),
                _gameplayWorld.SpawnBounds,
                animalSettings.PatrolPointReachDistance);

            var followState = new FollowHerdAnimalState(
                animalMovementService,
                ConfigMapper.ToAnimalMovementSettings(animalConfig),
                herdService,
                () => hero.Position,
                animalSettings.FollowDistance);

            var collectionService = new AnimalCollectionService(
                herdService,
                animalSettings);

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
                _animalDeliveryService,
                followState);

            var animalViewPool = new AnimalViewPool(
                sceneReferences.AnimalPrefab,
                sceneReferences.AnimalsContainer);

            _animalViewRegistry = new AnimalViewRegistry(animalViewPool);
            _heroViewBinder = new HeroViewBinder(hero, sceneReferences.HeroView);

            sceneReferences.ScoreView.Bind(scoreService);

            _animalSpawnService.Spawned += OnAnimalSpawned;
            _animalDeliveryService.Delivered += OnAnimalDelivered;

            SpawnInitialAnimals(patrolState);
            ResetSpawnTimer();
        }

        private void SpawnInitialAnimals(IAnimalState patrolState)
        {
            for (int i = 0; i < spawnerConfig.InitialSpawnCount; i++)
            {
                AnimalModel animal = _animalSpawnService.Spawn();
                animal.SetState(patrolState);
            }
        }

        private void UpdateSpawning(float deltaTime)
        {
            _spawnTimer -= deltaTime;

            if (_spawnTimer > 0f)
                return;

            if (_animalSpawnService.Animals.Count < spawnerConfig.MaxAliveAnimals)
            {
                AnimalModel animal = _animalSpawnService.Spawn();

                var movementService = new MovementService();
                var animalMovementService = new AnimalMovementService(movementService);

                var patrolState = new PatrolAnimalState(
                    animalMovementService,
                    ConfigMapper.ToAnimalMovementSettings(animalConfig),
                    _gameplayWorld.SpawnBounds,
                    animalConfig.PatrolPointReachDistance);

                animal.SetState(patrolState);
            }

            ResetSpawnTimer();
        }

        private void ResetSpawnTimer()
        {
            _spawnTimer = Random.Range(
                spawnerConfig.SpawnIntervalMin,
                spawnerConfig.SpawnIntervalMax);
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
