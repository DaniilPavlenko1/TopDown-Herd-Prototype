using Domain.Common;

namespace Domain.Animals
{
    public sealed class AnimalModel
    {
        public AnimalId Id { get; }
        public GameVector2 Position { get; private set; }
        public AnimalStatus Status { get; private set; }

        public AnimalModel(AnimalId id, GameVector2 startPosition)
        {
            Id = id;
            Position = startPosition;
            Status = AnimalStatus.Patrol;
        }

        public void SetPosition(GameVector2 position)
        {
            Position = position;
        }

        public void SetStatus(AnimalStatus status)
        {
            Status = status;
        }
    }
}
