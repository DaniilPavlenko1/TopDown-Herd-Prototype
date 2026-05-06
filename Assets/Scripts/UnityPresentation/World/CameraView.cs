using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraView : MonoBehaviour
    {
        private Camera _camera;

        public float AspectRatio => Camera.aspect;

        private Camera Camera
        {
            get
            {
                EnsureCamera();
                return _camera;
            }
        }

        private void Awake()
        {
            EnsureCamera();
        }

        public void Apply(WorldLayout layout)
        {
            Camera.orthographicSize = layout.WorldBounds.Size.Y * 0.5f;

            transform.position = UnityVectorMapper.ToVector3(
                layout.WorldBounds.Center,
                transform.position.z);
        }

        private void EnsureCamera()
        {
            if (_camera != null)
                return;

            _camera = GetComponent<Camera>();
            _camera.orthographic = true;
        }
    }
}
