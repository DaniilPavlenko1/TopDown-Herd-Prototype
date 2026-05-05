using Application.Animals;
using Application.Input;
using Domain.Animals.States;
using Domain.Hero;
using Domain.Movement;

namespace Application.Gameplay
{
    public sealed class GameplayUpdateService
    {
        private readonly IPlayerInput _input;
        private readonly HeroModel _hero;
        private readonly HeroMovementService _heroMovementService;
        private readonly AnimalSpawnService _spawnService;
        private readonly AnimalCollectionService _collectionService;
        private readonly AnimalDeliveryService _deliveryService;
        private readonly IAnimalState _followState;

        public GameplayUpdateService(
            IPlayerInput input,
            HeroModel hero,
            HeroMovementService heroMovementService,
            AnimalSpawnService spawnService,
            AnimalCollectionService collectionService,
            AnimalDeliveryService deliveryService,
            IAnimalState followState)
        {
            _input = input;
            _hero = hero;
            _heroMovementService = heroMovementService;
            _spawnService = spawnService;
            _collectionService = collectionService;
            _deliveryService = deliveryService;
            _followState = followState;
        }

        public void Tick(float deltaTime)
        {
            _input.Tick();

            _heroMovementService.Tick(_hero, deltaTime);

            for (int i = 0; i < _spawnService.Animals.Count; i++)
            {
                _spawnService.Animals[i].Tick(deltaTime);
            }

            _collectionService.TryCollectNearbyAnimals(
                _hero.Position,
                _spawnService.Animals,
                _followState);

            _deliveryService.TryDeliverAnimals();
        }
    }
}
