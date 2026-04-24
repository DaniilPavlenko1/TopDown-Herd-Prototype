using Animals;
using Configs;
using Herd;
using Hero;
using Input;
using UnityEngine;
using World;

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
        [SerializeField] private AnimalController[] testAnimals;
        [SerializeField] private AnimalConfig animalConfig;

        [Header("Herd")]
        [SerializeField] private HerdConfig herdConfig;

        [Header("World")]
        [SerializeField] private AdaptiveSpawnArea spawnArea;

        private MouseInputService _inputService;
        private IHerdService _herdService;

        private void Awake()
        {
            _inputService = new MouseInputService(mainCamera);
            _herdService = new HerdService(herdConfig);

            heroController.Construct(heroConfig, _inputService);

            foreach (AnimalController animal in testAnimals)
            {
                animal.Construct(
                    animalConfig,
                    spawnArea,
                    heroController.transform,
                    _herdService);
            }
        }

        private void Update()
        {
            _inputService.Tick();
        }
    }
}