namespace Animals.States
{
    public class AnimalStateMachine
    {
        private IAnimalState _currentState;

        public void ChangeState(IAnimalState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Tick(float deltaTime)
        {
            _currentState?.Tick(deltaTime);
        }
    }
}