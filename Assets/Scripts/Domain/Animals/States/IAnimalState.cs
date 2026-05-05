using Domain.Animals;

namespace Domain.Animals.States
{
    public interface IAnimalState
    {
        void Enter(AnimalModel animal);
        void Tick(AnimalModel animal, float deltaTime);
        void Exit(AnimalModel animal);
    }
}
