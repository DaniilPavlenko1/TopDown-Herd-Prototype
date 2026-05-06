namespace Domain.Animals.States
{
    public sealed class AnimalStateMachine
    {
        private IAnimalState _currentState;

        public void SetState(IAnimalState newState, AnimalModel animal)
        {
            _currentState?.Exit(animal);

            _currentState = newState;

            _currentState?.Enter(animal);
        }

        public void Tick(AnimalModel animal, float deltaTime)
        {
            _currentState?.Tick(animal, deltaTime);
        }
    }
}
