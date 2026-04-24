using Animals.States;
using Configs;
using UnityEngine;
using World;

namespace Animals
{
    public class AnimalController : MonoBehaviour
    {
        private AnimalConfig _config;
        private AnimalMover _mover;
        private AnimalStateMachine _stateMachine;

        public void Construct(AnimalConfig config, AdaptiveSpawnArea spawnArea)
        {
            _config = config;

            _mover = new AnimalMover(transform, _config.MoveSpeed);
            _stateMachine = new AnimalStateMachine();

            var patrolState = new PatrolAnimalState(
                transform,
                _mover,
                _config,
                spawnArea);

            _stateMachine.ChangeState(patrolState);
        }

        private void Update()
        {
            _stateMachine?.Tick(Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            if (_config == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _config.PatrolRadius);
        }
    }
}