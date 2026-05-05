using Domain.Common;
using Domain.Animals.States;

namespace Domain.Animals
{
    public sealed class AnimalModel
    {
        public AnimalId Id { get; }
        public GameVector2 Position { get; private set; }
        public AnimalStatus Status { get; private set; }

        private readonly AnimalStateMachine _stateMachine;

        public AnimalModel(AnimalId id, GameVector2 startPosition)
        {
            Id = id;
            Position = startPosition;
            Status = AnimalStatus.Patrol;

            _stateMachine = new AnimalStateMachine();
        }

        public void SetPosition(GameVector2 position)
        {
            Position = position;
        }

        public void SetStatus(AnimalStatus status)
        {
            Status = status;
        }

        public void SetState(IAnimalState state)
        {
            _stateMachine.SetState(state, this);
        }

        public void Tick(float deltaTime)
        {
            _stateMachine.Tick(this, deltaTime);
        }
    }
}
