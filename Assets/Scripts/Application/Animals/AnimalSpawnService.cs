using System.Collections.Generic;
using Application.Animals.Events;
using Application.Common;
using Domain.Animals;
using Domain.Common;
using Application.World;

namespace Application.Animals
{
    public sealed class AnimalSpawnService
    {
        private readonly IEventBus _eventBus;
        private readonly IRandomService _randomService;
        private readonly GameplayWorld _world;
        private readonly List<AnimalModel> _animals = new();

        private int _nextId;

        public IReadOnlyList<AnimalModel> Animals => _animals;

        public AnimalSpawnService(
            GameplayWorld world,
            IEventBus eventBus,
            IRandomService randomService)
        {
            _world = world;
            _eventBus = eventBus;
            _randomService = randomService;
        }

        public AnimalModel Spawn()
        {
            AnimalModel animal = new AnimalModel(
                new AnimalId(_nextId++),
                GetRandomSpawnPosition());

            _animals.Add(animal);
            _eventBus.Publish(new AnimalSpawnedEvent(animal));

            return animal;
        }

        public void Despawn(AnimalModel animal)
        {
            if (animal == null)
                return;

            _animals.Remove(animal);
        }

        private GameVector2 GetRandomSpawnPosition()
        {
            float x = _randomService.Range(
                _world.SpawnBounds.MinX,
                _world.SpawnBounds.MaxX);

            float y = _randomService.Range(
                _world.SpawnBounds.MinY,
                _world.SpawnBounds.MaxY);

            return new GameVector2(x, y);
        }
    }
}
