using Domain.Animals;

namespace Application.Animals.Events
{
    public readonly struct AnimalDeliveredEvent
    {
        public AnimalModel Animal { get; }

        public AnimalDeliveredEvent(AnimalModel animal)
        {
            Animal = animal;
        }
    }
}
