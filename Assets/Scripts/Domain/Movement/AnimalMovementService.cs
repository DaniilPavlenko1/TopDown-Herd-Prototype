using Domain.Animals;
using Domain.Common;

namespace Domain.Movement
{
    public sealed class AnimalMovementService
    {
        private readonly MovementService _movementService;

        public AnimalMovementService(MovementService movementService)
        {
            _movementService = movementService;
        }

        public bool MoveAnimalTowards(
            AnimalModel animal,
            GameVector2 target,
            MovementSettings settings,
            float deltaTime)
        {
            GameVector2 newPosition = _movementService.MoveTowards(
                animal.Position,
                target,
                settings,
                deltaTime,
                out bool reached);

            animal.SetPosition(newPosition);

            return reached;
        }
    }
}
