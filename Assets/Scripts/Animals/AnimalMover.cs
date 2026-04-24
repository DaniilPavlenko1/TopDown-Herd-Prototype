using UnityEngine;

namespace Animals
{
    public class AnimalMover
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public AnimalMover(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void MoveTo(Vector3 target, float deltaTime)
        {
            Vector3 direction = target - _transform.position;
            direction.z = 0f;

            if (direction.sqrMagnitude <= 0.001f)
                return;

            _transform.position += direction.normalized * _speed * deltaTime;
        }
    }
}