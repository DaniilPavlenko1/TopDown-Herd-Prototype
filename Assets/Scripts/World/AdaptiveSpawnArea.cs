using UnityEngine;

namespace World
{
    public class AdaptiveSpawnArea : MonoBehaviour
    {
        [SerializeField] private WorldBounds worldBounds;
        [SerializeField] private AdaptiveYardZone yardZone;
        [SerializeField] private float leftPadding = 1f;
        [SerializeField] private float rightGapFromYard = 1f;
        [SerializeField] private float verticalPadding = 1f;

        private Vector2 _size;

        public Bounds Bounds => new Bounds(transform.position, _size);

        public Vector3 Clamp(Vector3 position)
        {
            Bounds bounds = Bounds;

            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
            position.z = 0f;

            return position;
        }

        private void Awake()
        {
            Apply();
        }

        private void Start()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Apply();
        }
#endif

        public void Apply()
        {
            if (worldBounds == null || yardZone == null)
                return;

            Bounds yardBounds = new Bounds(
                yardZone.transform.position,
                yardZone.transform.localScale);

            float minX = worldBounds.Min.x + leftPadding;
            float maxX = yardBounds.min.x - rightGapFromYard;

            float minY = worldBounds.Min.y + verticalPadding;
            float maxY = worldBounds.Max.y - verticalPadding;

            float width = Mathf.Max(1f, maxX - minX);
            float height = Mathf.Max(1f, maxY - minY);

            _size = new Vector2(width, height);

            transform.position = new Vector3(
                minX + width * 0.5f,
                minY + height * 0.5f,
                0f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, _size);
        }
    }
}