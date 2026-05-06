using Domain.Common;
using Domain.Movement;

namespace Domain.Animals.States
{
    public sealed class PatrolAnimalState : IAnimalState
    {
        private readonly AnimalMovementService _movementService;
        private readonly MovementSettings _movementSettings;
        private readonly GameBounds _allowedBounds;
        private readonly float _patrolRadius;
        private readonly float _reachDistance;
        private readonly IRandomService _randomService;

        public PatrolAnimalState(
            AnimalMovementService movementService,
            MovementSettings movementSettings,
            GameBounds allowedBounds,
            float patrolRadius,
            float reachDistance,
            IRandomService randomService)
        {
            _movementService = movementService;
            _movementSettings = movementSettings;
            _allowedBounds = allowedBounds;
            _patrolRadius = patrolRadius;
            _reachDistance = reachDistance;
            _randomService = randomService;
        }

        private GameVector2 _origin;
        private GameVector2 _target;

        public void Enter(AnimalModel animal)
        {
            _origin = _allowedBounds.Clamp(animal.Position);
            animal.SetPosition(_origin);
            animal.SetStatus(AnimalStatus.Patrol);

            PickNewTarget();
        }

        public void Tick(AnimalModel animal, float deltaTime)
        {
            float distanceToTarget = GameVector2.Distance(
                animal.Position,
                _target);

            if (distanceToTarget <= _reachDistance)
            {
                PickNewTarget();
                return;
            }

            _movementService.MoveAnimalTowards(
                animal,
                _target,
                _movementSettings,
                deltaTime);
        }

        public void Exit(AnimalModel animal)
        {
        }

        private void PickNewTarget()
        {
            float angle = _randomService.Range01() * System.MathF.PI * 2f;
            float radius = System.MathF.Sqrt(_randomService.Range01()) * _patrolRadius;

            var offset = new GameVector2(
                System.MathF.Cos(angle) * radius,
                System.MathF.Sin(angle) * radius);

            _target = _allowedBounds.Clamp(_origin + offset);
        }
    }
}
