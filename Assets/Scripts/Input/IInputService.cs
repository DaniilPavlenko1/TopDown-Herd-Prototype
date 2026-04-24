using System;

namespace Input
{
    public interface IInputService
    {
        event Action<UnityEngine.Vector3> OnMoveCommand;
    }
}