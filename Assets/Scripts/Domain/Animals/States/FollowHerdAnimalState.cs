using System;
using Domain.Common;
using Domain.Herd;
using Domain.Movement;

namespace Domain.Animals.States
{
    public sealed class FollowHerdAnimalState : IAnimalState
    {
        private readonly AnimalMovementService _movementService;
        private readonly MovementSettings _movementSettings;
        private readonly IHerdService _herdService;
        private readonly Func<GameVector2> _heroPositionProvider;
        private readonly float _followDistance;

        public FollowHerdAnimalState(
            AnimalMovementService movementService,
            MovementSettings movementSettings,
            IHerdService herdService,
            Func<GameVector2> heroPositionProvider,
            float followDistance)
        {
            _movementService = movementService;
            _movementSettings = movementSettings;
            _herdService = herdService;
            _heroPositionProvider = heroPositionProvider;
            _followDistance = followDistance;
        }

        public void Enter(AnimalModel animal)
        {
            animal.SetStatus(AnimalStatus.Follow);
        }

        public void Tick(AnimalModel animal, float deltaTime)
        {
            int index = _herdService.GetIndexOf(animal);

            GameVector2 target = index <= 0
                ? _heroPositionProvider()
                : _herdService.Animals[index - 1].Position;

            float distance = GameVector2.Distance(animal.Position, target);

            if (distance <= _followDistance)
                return;

            _movementService.MoveAnimalTowards(
                animal,
                target,
                _movementSettings,
                deltaTime);
        }

        public void Exit(AnimalModel animal)
        {
        }
    }
}
