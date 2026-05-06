using System;
using Application.Animals.Events;
using Application.Common;

namespace Application.Animals
{
    public sealed class AnimalDeliveredDespawnHandler : IDisposableService
    {
        private readonly AnimalSpawnService _spawnService;
        private readonly IDisposable _subscription;

        public AnimalDeliveredDespawnHandler(
            IEventBus eventBus,
            AnimalSpawnService spawnService)
        {
            _spawnService = spawnService;
            _subscription = eventBus.Subscribe<AnimalDeliveredEvent>(OnAnimalDelivered);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnAnimalDelivered(AnimalDeliveredEvent eventData)
        {
            _spawnService.Despawn(eventData.Animal);
        }
    }
}
