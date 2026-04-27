using UnityEngine;
using System;

namespace Input
{
    public class MouseInputService : IInputService
    {
        public event Action<Vector3> OnMoveCommand;

        private readonly Camera _camera;

        public MouseInputService(Camera camera)
        {
            _camera = camera;
        }

        public void Tick()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(0)) return;
            
            var worldPos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            worldPos.z = 0f;

            OnMoveCommand?.Invoke(worldPos);
        }
    }
}