using System;
using Application.Input;
using Domain.Common;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.Input
{
    public sealed class MouseInputAdapter : IPlayerInput
    {
        public event Action<GameVector2> MoveCommand;

        private readonly Camera _camera;

        public MouseInputAdapter(Camera camera)
        {
            _camera = camera;
        }

        public void Tick()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(0))
                return;

            Vector3 worldPosition = _camera.ScreenToWorldPoint(
                UnityEngine.Input.mousePosition);

            worldPosition.z = 0f;

            MoveCommand?.Invoke(
                UnityVectorMapper.ToGameVector2(worldPosition));
        }
    }
}
