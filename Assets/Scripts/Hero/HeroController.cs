using Configs;
using Input;
using UnityEngine;

namespace Hero
{
    public class HeroController : MonoBehaviour
    {
        private HeroMover _mover;
        private IInputService _inputService;

        public void Construct(HeroConfig config, IInputService inputService)
        {
            _mover = new HeroMover(transform, config.MoveSpeed);
            _inputService = inputService;

            _inputService.OnMoveCommand += OnMoveCommand;
        }

        private void OnMoveCommand(Vector3 position)
        {
            _mover.SetTarget(position);
        }

        private void Update()
        {
            _mover.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (_inputService != null)
                _inputService.OnMoveCommand -= OnMoveCommand;
        }
    }
}