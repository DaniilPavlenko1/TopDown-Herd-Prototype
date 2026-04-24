using Configs;
using UnityEngine;
using World;

namespace Animals.States
{
    public class PatrolAnimalState : IAnimalState
    {
        private readonly Transform _animalTransform;
        private readonly AnimalMover _mover;
        private readonly AnimalConfig _config;
        private readonly AdaptiveSpawnArea _spawnArea;

        private Vector3 _spawnPosition;
        private Vector3 _targetPosition;
        private float _waitTimer;

        public PatrolAnimalState(
            Transform animalTransform,
            AnimalMover mover,
            AnimalConfig config,
            AdaptiveSpawnArea spawnArea)
        {
            _animalTransform = animalTransform;
            _mover = mover;
            _config = config;
            _spawnArea = spawnArea;
        }

        public void Enter()
        {
            _spawnPosition = _spawnArea.Clamp(_animalTransform.position);
            _animalTransform.position = _spawnPosition;

            PickNewTarget();
        }

        public void Tick(float deltaTime)
        {
            if (_waitTimer > 0f)
            {
                _waitTimer -= deltaTime;
                return;
            }

            _mover.MoveTo(_targetPosition, deltaTime);

            if (Vector3.Distance(_animalTransform.position, _targetPosition) <= _config.PatrolPointReachDistance)
            {
                _waitTimer = Random.Range(0.5f, 1.5f);
                PickNewTarget();
            }
        }

        public void Exit()
        {
        }

        private void PickNewTarget()
        {
            Vector2 randomPoint = Random.insideUnitCircle * _config.PatrolRadius;

            Vector3 rawTarget = _spawnPosition + new Vector3(
                randomPoint.x,
                randomPoint.y,
                0f);

            _targetPosition = _spawnArea.Clamp(rawTarget);
        }
    }
}