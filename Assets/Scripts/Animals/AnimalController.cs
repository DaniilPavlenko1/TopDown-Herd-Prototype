using Animals.States;
using Configs;
using Herd;
using UnityEngine;
using World;

namespace Animals
{
    public class AnimalController : MonoBehaviour
    {
        private AnimalConfig _config;
        private AnimalMover _mover;
        private AnimalStateMachine _stateMachine;

        private Transform _heroTransform;
        private IHerdService _herdService;
        private AdaptiveSpawnArea _spawnArea;

        public void Construct(
            AnimalConfig config,
            AdaptiveSpawnArea spawnArea,
            Transform heroTransform,
            IHerdService herdService)
        {
            _config = config;
            _spawnArea = spawnArea;
            _heroTransform = heroTransform;
            _herdService = herdService;

            _mover = new AnimalMover(transform, _config.MoveSpeed);
            _stateMachine = new AnimalStateMachine();

            SwitchToPatrol();
        }

        public void TryCollect()
        {
            if (_herdService.TryAddAnimal(this))
            {
                SwitchToFollow();
            }
        }

        public void Deliver()
        {
            var deliveredState = new DeliveredAnimalState(gameObject);
            _stateMachine.ChangeState(deliveredState);
        }

        private void SwitchToPatrol()
        {
            var patrolState = new PatrolAnimalState(
                transform,
                _mover,
                _config,
                _spawnArea);

            _stateMachine.ChangeState(patrolState);
        }

        private void SwitchToFollow()
        {
            var followState = new FollowHeroAnimalState(
                this,
                transform,
                _heroTransform,
                _mover,
                _config,
                _herdService);

            _stateMachine.ChangeState(followState);
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

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _config.CollectRadius);
        }
    }
}