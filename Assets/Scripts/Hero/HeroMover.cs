using UnityEngine;

namespace Hero
{
    public class HeroMover
    {
        private Transform _transform;
        private float _speed;

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

            Vector3 direction = (_target - _transform.position);
            float distance = direction.magnitude;

            if (distance < 0.05f)
            {
                _hasTarget = false;
                return;
            }

            direction.Normalize();
            _transform.position += direction * _speed * deltaTime;
        }
    }
}