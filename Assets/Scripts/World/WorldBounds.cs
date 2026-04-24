using UnityEngine;

namespace World
{
    public class WorldBounds : MonoBehaviour
    {
        [SerializeField] private Vector2 size = new Vector2(18f, 10f);

        public Bounds Bounds => new Bounds(transform.position, size);

        public float Width => size.x;
        public float Height => size.y;
        public Vector3 Center => transform.position;
        public Vector3 Min => Bounds.min;
        public Vector3 Max => Bounds.max;

        public void SetSize(Vector2 newSize)
        {
            size = newSize;
        }

        public Vector3 Clamp(Vector3 position)
        {
            Bounds bounds = Bounds;

            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
            position.z = 0f;

            return position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}