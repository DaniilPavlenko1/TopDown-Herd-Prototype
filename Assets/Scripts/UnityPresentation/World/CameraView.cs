using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraView : MonoBehaviour
    {
        private Camera _camera;

        public float AspectRatio => _camera.aspect;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _camera.orthographic = true;
        }

        public void Apply(WorldLayout layout)
        {
            _camera.orthographicSize = layout.WorldBounds.Size.Y * 0.5f;

            transform.position = UnityVectorMapper.ToVector3(
                layout.WorldBounds.Center,
                transform.position.z);
        }
    }
}
