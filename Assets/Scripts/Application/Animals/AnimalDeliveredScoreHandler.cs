using System;
using Application.Animals.Events;
using Application.Common;
using Domain.Score;

namespace Application.Animals
{
    public sealed class AnimalDeliveredScoreHandler : IDisposableService
    {
        private readonly IScoreService _scoreService;
        private readonly IDisposable _subscription;

        public AnimalDeliveredScoreHandler(
            IEventBus eventBus,
            IScoreService scoreService)
        {
            _scoreService = scoreService;
            _subscription = eventBus.Subscribe<AnimalDeliveredEvent>(OnAnimalDelivered);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnAnimalDelivered(AnimalDeliveredEvent eventData)
        {
            _scoreService.Add(1);
        }
    }
}
