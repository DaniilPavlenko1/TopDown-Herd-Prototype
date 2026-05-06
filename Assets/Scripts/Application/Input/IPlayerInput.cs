using System;
using Domain.Common;

namespace Application.Input
{
    public interface IPlayerInput
    {
        event Action<GameVector2> MoveCommand;

        void Tick();
    }
}
