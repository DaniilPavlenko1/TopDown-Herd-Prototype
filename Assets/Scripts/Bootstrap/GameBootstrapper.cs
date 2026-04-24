using Animals;
using Configs;
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

        [Header("World")]
        [SerializeField] private AdaptiveSpawnArea spawnArea;

        private MouseInputService _inputService;

        private void Awake()
        {
            _inputService = new MouseInputService(mainCamera);

            heroController.Construct(heroConfig, _inputService);

            foreach (AnimalController animal in testAnimals)
            {
                animal.Construct(animalConfig, spawnArea);
            }
        }

        private void Update()
        {
            _inputService.Tick();
        }
    }
}