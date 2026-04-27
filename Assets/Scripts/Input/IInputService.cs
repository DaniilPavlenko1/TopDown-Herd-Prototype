using System;
using UnityEngine;

namespace Input
{
    public interface IInputService
    {
        event Action<Vector3> OnMoveCommand;

        void Tick();
    }
}