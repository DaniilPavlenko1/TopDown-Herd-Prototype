using Domain.Animals;

namespace Application.Animals.Events
{
    public readonly struct AnimalSpawnedEvent
    {
        public AnimalModel Animal { get; }

        public AnimalSpawnedEvent(AnimalModel animal)
        {
            Animal = animal;
        }
    }
}
