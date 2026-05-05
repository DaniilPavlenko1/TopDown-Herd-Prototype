namespace Domain.Animals.States
{
    public interface IAnimalStateFactory
    {
        IAnimalState CreatePatrolState();
        IAnimalState CreateFollowState();
    }
}
