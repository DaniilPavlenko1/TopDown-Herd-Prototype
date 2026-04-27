using Configs;
using Herd;
using UnityEngine;
using World;

namespace Animals
{
    public class AnimalSpawner
    {
        private readonly AnimalPool _pool;
        private readonly AnimalConfig _animalConfig;
        private readonly SpawnerConfig _spawnerConfig;
        private readonly AdaptiveSpawnArea _spawnArea;
        private readonly Transform _heroTransform;
        private readonly IHerdService _herdService;

        private float _timer;

        public AnimalSpawner(
            AnimalPool pool,
            AnimalConfig animalConfig,
            SpawnerConfig spawnerConfig,
            AdaptiveSpawnArea spawnArea,
            Transform heroTransform,
            IHerdService herdService)
        {
            _pool = pool;
            _animalConfig = animalConfig;
            _spawnerConfig = spawnerConfig;
            _spawnArea = spawnArea;
            _heroTransform = heroTransform;
            _herdService = herdService;

            ResetTimer();
        }

        public void SpawnInitial()
        {
            for (int i = 0; i < _spawnerConfig.InitialSpawnCount; i++)
                Spawn();
        }

        public void Tick(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer > 0f)
                return;

            Spawn();
            ResetTimer();
        }

        private void Spawn()
        {
            AnimalController animal = _pool.Get();

            animal.transform.position = GetRandomPoint();
            animal.Construct(
                _animalConfig,
                _spawnArea,
                _heroTransform,
                _herdService,
                _pool);
        }

        private Vector3 GetRandomPoint()
        {
            Bounds bounds = _spawnArea.Bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            return new Vector3(x, y, 0f);
        }

        private void ResetTimer()
        {
            _timer = Random.Range(
                _spawnerConfig.SpawnIntervalMin,
                _spawnerConfig.SpawnIntervalMax);
        }
    }
}