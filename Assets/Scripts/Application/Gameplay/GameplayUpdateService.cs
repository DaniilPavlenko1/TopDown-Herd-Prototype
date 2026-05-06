using Application.Animals;
using Application.Common;
using Application.Input;
using Domain.Hero;
using Domain.Movement;

namespace Application.Gameplay
{
    public sealed class GameplayUpdateService : ITickable
    {
        private readonly IPlayerInput _input;
        private readonly HeroModel _hero;
        private readonly HeroMovementService _heroMovementService;
        private readonly AnimalSpawnService _spawnService;
        private readonly AnimalCollectionService _collectionService;
        private readonly AnimalDeliveryService _deliveryService;

        public GameplayUpdateService(
            IPlayerInput input,
            HeroModel hero,
            HeroMovementService heroMovementService,
            AnimalSpawnService spawnService,
            AnimalCollectionService collectionService,
            AnimalDeliveryService deliveryService)
        {
            _input = input;
            _hero = hero;
            _heroMovementService = heroMovementService;
            _spawnService = spawnService;
            _collectionService = collectionService;
            _deliveryService = deliveryService;
        }

        public void Tick(float deltaTime)
        {
            _input.Tick();

            _heroMovementService.Tick(_hero, deltaTime);

            for (int i = 0; i < _spawnService.Animals.Count; i++)
                _spawnService.Animals[i].Tick(deltaTime);

            _collectionService.TryCollectNearbyAnimals(
                _hero.Position,
                _spawnService.Animals);

            _deliveryService.TryDeliverAnimals();
        }
    }
}
