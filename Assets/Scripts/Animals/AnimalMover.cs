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
            target.z = 0f;

            _transform.position = Vector3.MoveTowards(
                _transform.position,
                target,
                _speed * deltaTime);
        }
    }
}