using System;
using Application.World;
using Domain.Animals;
using Domain.Animals.States;
using Domain.Common;
using Domain.Herd;
using Domain.Movement;

namespace Application.Animals
{
    public sealed class AnimalStateFactory : IAnimalStateFactory
    {
        private readonly AnimalMovementService _animalMovementService;
        private readonly MovementSettings _movementSettings;
        private readonly GameplayWorld _world;
        private readonly IHerdService _herdService;
        private readonly Func<GameVector2> _heroPositionProvider;
        private readonly AnimalSettings _animalSettings;

        public AnimalStateFactory(
            AnimalMovementService animalMovementService,
            MovementSettings movementSettings,
            GameplayWorld world,
            IHerdService herdService,
            Func<GameVector2> heroPositionProvider,
            AnimalSettings animalSettings)
        {
            _animalMovementService = animalMovementService;
            _movementSettings = movementSettings;
            _world = world;
            _herdService = herdService;
            _heroPositionProvider = heroPositionProvider;
            _animalSettings = animalSettings;
        }

        public IAnimalState CreatePatrolState()
        {
            return new PatrolAnimalState(
                _animalMovementService,
                _movementSettings,
                _world.SpawnBounds,
                _animalSettings.PatrolPointReachDistance);
        }

        public IAnimalState CreateFollowState()
        {
            return new FollowHerdAnimalState(
                _animalMovementService,
                _movementSettings,
                _herdService,
                _heroPositionProvider,
                _animalSettings.FollowDistance);
        }
    }
}
