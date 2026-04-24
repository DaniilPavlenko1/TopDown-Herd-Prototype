using Configs;
using Hero;
using Input;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private HeroController heroController;
        [SerializeField] private HeroConfig heroConfig;

        private MouseInputService _inputService;

        private void Awake()
        {
            _inputService = new MouseInputService(mainCamera);
            
            heroController.Construct(heroConfig, _inputService);
        }

        private void Update()
        {
            _inputService.Tick();
        }
    }
}