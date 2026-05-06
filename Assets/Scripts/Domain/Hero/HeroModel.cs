using Domain.Common;

namespace Domain.Hero
{
    public sealed class HeroModel
    {
        public GameVector2 Position { get; private set; }
        public GameVector2 TargetPosition { get; private set; }
        public bool HasTarget { get; private set; }

        public HeroModel(GameVector2 startPosition)
        {
            Position = startPosition;
            TargetPosition = startPosition;
        }

        public void SetTarget(GameVector2 targetPosition)
        {
            TargetPosition = targetPosition;
            HasTarget = true;
        }

        public void SetPosition(GameVector2 position)
        {
            Position = position;
        }

        public bool IsMoving()
        {
            return HasTarget && !Position.Equals(TargetPosition);
        }

        public void ClearTarget()
        {
            HasTarget = false;
            TargetPosition = Position;
        }
    }
}
