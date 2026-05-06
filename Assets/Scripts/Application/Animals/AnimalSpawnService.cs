using System;
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
        private readonly GameplayWorld _world;
        private readonly List<AnimalModel> _animals = new();
        private readonly Random _random = new();

        private int _nextId;

        public IReadOnlyList<AnimalModel> Animals => _animals;

        public AnimalSpawnService(
            GameplayWorld world,
            IEventBus eventBus)
        {
            _world = world;
            _eventBus = eventBus;
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
            float x = (float)_random.NextDouble() *
                (_world.SpawnBounds.MaxX - _world.SpawnBounds.MinX) +
                _world.SpawnBounds.MinX;

            float y = (float)_random.NextDouble() *
                (_world.SpawnBounds.MaxY - _world.SpawnBounds.MinY) +
                _world.SpawnBounds.MinY;

            return new GameVector2(x, y);
        }
    }
}
