using System;
using Application.Common;
using Domain.Animals;
using Domain.Animals.States;

namespace Application.Animals
{
    public sealed class AnimalSpawnTimerService : ITickable, IInitializable
    {
        private readonly AnimalSpawnService _spawnService;
        private readonly IAnimalState _patrolState;
        private readonly int _initialSpawnCount;
        private readonly int _maxAliveAnimals;
        private readonly float _spawnIntervalMin;
        private readonly float _spawnIntervalMax;
        private readonly Random _random = new();

        private float _timer;

        public AnimalSpawnTimerService(
            AnimalSpawnService spawnService,
            IAnimalState patrolState,
            int initialSpawnCount,
            int maxAliveAnimals,
            float spawnIntervalMin,
            float spawnIntervalMax)
        {
            _spawnService = spawnService;
            _patrolState = patrolState;
            _initialSpawnCount = initialSpawnCount;
            _maxAliveAnimals = maxAliveAnimals;
            _spawnIntervalMin = spawnIntervalMin;
            _spawnIntervalMax = spawnIntervalMax;
        }

        public void Initialize()
        {
            for (int i = 0; i < _initialSpawnCount; i++)
                SpawnAnimal();

            ResetTimer();
        }

        public void Tick(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer > 0f)
                return;

            if (_spawnService.Animals.Count < _maxAliveAnimals)
                SpawnAnimal();

            ResetTimer();
        }

        private void SpawnAnimal()
        {
            AnimalModel animal = _spawnService.Spawn();
            animal.SetState(_patrolState);
        }

        private void ResetTimer()
        {
            float t = (float)_random.NextDouble();
            _timer = _spawnIntervalMin + (_spawnIntervalMax - _spawnIntervalMin) * t;
        }
    }
}
