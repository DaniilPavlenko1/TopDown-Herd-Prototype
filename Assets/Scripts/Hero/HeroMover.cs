using UnityEngine;

namespace Hero
{
    public class HeroMover
    {
        private readonly Transform _transform;
        private readonly float _speed;

        private Vector3 _target;
        private bool _hasTarget;

        public HeroMover(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void SetTarget(Vector3 target)
        {
            _target = target;
            _hasTarget = true;
        }

        public void Tick(float deltaTime)
        {
            if (!_hasTarget) return;

            _transform.position = Vector3.MoveTowards(
                _transform.position,
                _target,
                _speed * deltaTime);

            if (Vector3.Distance(_transform.position, _target) <= 0.01f)
                _hasTarget = false;
        }
    }
}