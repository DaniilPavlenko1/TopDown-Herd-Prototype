using UnityEngine;

namespace World
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AdaptiveYardZone : MonoBehaviour
    {
        [SerializeField] private WorldBounds worldBounds;
        [SerializeField, Range(0.1f, 0.4f)] private float widthPercent = 0.18f;
        [SerializeField, Range(0.2f, 0.9f)] private float heightPercent = 0.45f;
        [SerializeField] private float rightPadding = 1f;

        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            Apply();
        }

        private void Start()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider2D>();
            Apply();
        }
#endif

        public void Apply()
        {
            if (worldBounds == null || _collider == null)
                return;

            float width = worldBounds.Width * widthPercent;
            float height = worldBounds.Height * heightPercent;

            float x = worldBounds.Max.x - rightPadding - width * 0.5f;
            float y = worldBounds.Center.y;

            transform.position = new Vector3(x, y, 0f);
            transform.localScale = new Vector3(width, height, 1f);

            _collider.isTrigger = true;
            _collider.size = Vector2.one;
            _collider.offset = Vector2.zero;
        }
    }
}