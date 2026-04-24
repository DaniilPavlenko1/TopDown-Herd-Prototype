namespace Animals.States
{
    public interface IAnimalState
    {
        void Enter();
        void Tick(float deltaTime);
        void Exit();
    }
}