using System;
using Application.Common;
using Domain.Animals;
using Domain.Animals.States;

namespace Application.Animals
{
    public sealed class AnimalSpawnTimerService : ITickable, IInitializable
    {
        private readonly AnimalSpawnService _spawnService;
        private readonly IAnimalStateFactory _stateFactory;
        private readonly int _initialSpawnCount;
        private readonly int _maxAliveAnimals;
        private readonly float _spawnIntervalMin;
        private readonly float _spawnIntervalMax;
        private readonly Random _random = new();

        private float _timer;

        public AnimalSpawnTimerService(
            AnimalSpawnService spawnService,
            IAnimalStateFactory stateFactory,
            int initialSpawnCount,
            int maxAliveAnimals,
            float spawnIntervalMin,
            float spawnIntervalMax)
        {
            _spawnService = spawnService;
            _stateFactory = stateFactory;
            _initialSpawnCount = initialSpawnCount;
            _maxAliveAnimals = maxAliveAnimals;
            _spawnIntervalMin = spawnIntervalMin;
            _spawnIntervalMax = spawnIntervalMax;
        }

        public void Initialize()
        {
            int spawnCount = Math.Min(_initialSpawnCount, _maxAliveAnimals);

            for (int i = 0; i < spawnCount; i++)
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
            animal.SetState(_stateFactory.CreatePatrolState());
        }

        private void ResetTimer()
        {
            float t = (float)_random.NextDouble();
            _timer = _spawnIntervalMin + (_spawnIntervalMax - _spawnIntervalMin) * t;
        }
    }
}
