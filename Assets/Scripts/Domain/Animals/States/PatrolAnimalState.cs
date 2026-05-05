using System;
using Domain.Common;
using Domain.Movement;

namespace Domain.Animals.States
{
    public sealed class PatrolAnimalState : IAnimalState
    {
        private readonly AnimalMovementService _movementService;
        private readonly MovementSettings _movementSettings;
        private readonly GameBounds _bounds;
        private readonly float _reachDistance;

        private GameVector2 _target;
        private readonly Random _random = new();

        public PatrolAnimalState(
            AnimalMovementService movementService,
            MovementSettings movementSettings,
            GameBounds bounds,
            float reachDistance)
        {
            _movementService = movementService;
            _movementSettings = movementSettings;
            _bounds = bounds;
            _reachDistance = reachDistance;
        }

        public void Enter(AnimalModel animal)
        {
            SetNewTarget(animal);
            animal.SetStatus(AnimalStatus.Patrol);
        }

        public void Tick(AnimalModel animal, float deltaTime)
        {
            bool reached = _movementService.MoveAnimalTowards(
                animal,
                _target,
                _movementSettings,
                deltaTime);

            if (reached)
                SetNewTarget(animal);
        }

        public void Exit(AnimalModel animal)
        {
        }

        private void SetNewTarget(AnimalModel animal)
        {
            float x = (float)_random.NextDouble() * (_bounds.MaxX - _bounds.MinX) + _bounds.MinX;
            float y = (float)_random.NextDouble() * (_bounds.MaxY - _bounds.MinY) + _bounds.MinY;

            _target = new GameVector2(x, y);
        }
    }
}
