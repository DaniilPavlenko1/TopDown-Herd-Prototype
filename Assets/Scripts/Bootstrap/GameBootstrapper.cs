using Animals;
using Configs;
using Delivery;
using Herd;
using Hero;
using Input;
using Score;
using UI;
using UnityEngine;
using World;
using Yard;

namespace Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;

        [Header("Hero")]
        [SerializeField] private HeroController heroController;
        [SerializeField] private HeroConfig heroConfig;

        [Header("Animals")]
        [SerializeField] private AnimalController animalPrefab;
        [SerializeField] private AnimalConfig animalConfig;
        [SerializeField] private Transform animalsContainer;

        [Header("Spawner")]
        [SerializeField] private SpawnerConfig spawnerConfig;

        [Header("Herd")]
        [SerializeField] private HerdConfig herdConfig;

        [Header("World")]
        [SerializeField] private AdaptiveSpawnArea spawnArea;

        [Header("Delivery")]
        [SerializeField] private YardZone yardZone;

        [Header("UI")]
        [SerializeField] private ScoreView scoreView;

        private MouseInputService _inputService;
        private IHerdService _herdService;
        private IScoreService _scoreService;
        private DeliveryService _deliveryService;

        private AnimalPool _animalPool;
        private AnimalSpawner _animalSpawner;

        private void Awake()
        {
            _inputService = new MouseInputService(mainCamera);

            _herdService = new HerdService(herdConfig);
            _scoreService = new ScoreService();

            _deliveryService = new DeliveryService(
                _herdService,
                _scoreService);

            _animalPool = new AnimalPool(
                animalPrefab,
                animalsContainer);

            _animalSpawner = new AnimalSpawner(
                _animalPool,
                animalConfig,
                spawnerConfig,
                spawnArea,
                heroController.transform,
                _herdService);

            heroController.Construct(heroConfig, _inputService);
            scoreView.Construct(_scoreService);

            yardZone.AnimalEntered += OnAnimalEnteredYard;

            _animalSpawner.SpawnInitial();
        }

        private void Update()
        {
            _inputService.Tick();
            _animalSpawner.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (yardZone != null)
                yardZone.AnimalEntered -= OnAnimalEnteredYard;
        }

        private void OnAnimalEnteredYard(AnimalController animal)
        {
            _deliveryService.DeliverAnimal(animal);
        }
    }
}