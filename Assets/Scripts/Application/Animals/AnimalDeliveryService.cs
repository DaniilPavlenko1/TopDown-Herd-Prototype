using Application.Animals.Events;
using Application.Common;
using Domain.Animals;
using Domain.Herd;
using Application.World;

namespace Application.Animals
{
    public sealed class AnimalDeliveryService
    {
        private readonly IEventBus _eventBus;
        private readonly IHerdService _herdService;
        private readonly GameplayWorld _world;

        public AnimalDeliveryService(
            IHerdService herdService,
            GameplayWorld world,
            IEventBus eventBus)
        {
            _herdService = herdService;
            _world = world;
            _eventBus = eventBus;
        }

        public void TryDeliverAnimals()
        {
            for (int i = _herdService.Animals.Count - 1; i >= 0; i--)
            {
                AnimalModel animal = _herdService.Animals[i];

                if (!_world.YardBounds.Contains(animal.Position))
                    continue;

                _herdService.Remove(animal);
                animal.SetStatus(AnimalStatus.Delivered);
                _eventBus.Publish(new AnimalDeliveredEvent(animal));
            }
        }
    }
}
