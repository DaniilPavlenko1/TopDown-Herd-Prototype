using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Camera))]
    public class CameraWorldAdapter : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private float referenceOrthographicSize = 5f;

        [Header("World")]
        [SerializeField] private Transform ground;
        [SerializeField] private WorldBounds worldBounds;

        [Header("Padding")]
        [SerializeField] private float horizontalPadding = 0.5f;
        [SerializeField] private float verticalPadding = 0.5f;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            Apply();
        }

        private void Start()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _camera = GetComponent<Camera>();

            if (_camera != null)
                Apply();
        }
#endif

        private void Apply()
        {
            if (_camera == null)
                return;

            _camera.orthographic = true;
            _camera.orthographicSize = referenceOrthographicSize;

            float visibleHeight = _camera.orthographicSize * 2f;
            float visibleWidth = visibleHeight * _camera.aspect;

            Vector3 cameraPosition = transform.position;
            cameraPosition.x = 0f;
            cameraPosition.y = 0f;
            transform.position = cameraPosition;

            if (ground != null)
            {
                ground.position = Vector3.zero;
                ground.localScale = new Vector3(
                    visibleWidth + horizontalPadding,
                    visibleHeight + verticalPadding,
                    1f);
            }

            if (worldBounds != null)
            {
                worldBounds.SetSize(new Vector2(
                    visibleWidth,
                    visibleHeight));
            }
        }
    }
}