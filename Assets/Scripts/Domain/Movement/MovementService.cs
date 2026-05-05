using Domain.Common;

namespace Domain.Movement
{
    public sealed class MovementService
    {
        public GameVector2 MoveTowards(
            GameVector2 current,
            GameVector2 target,
            MovementSettings settings,
            float deltaTime,
            out bool reached)
        {
            float distance = GameVector2.Distance(current, target);

            if (distance <= settings.StopDistance)
            {
                reached = true;
                return target;
            }

            reached = false;

            return GameMath.MoveTowards(
                current,
                target,
                settings.Speed * deltaTime);
        }
    }
}
