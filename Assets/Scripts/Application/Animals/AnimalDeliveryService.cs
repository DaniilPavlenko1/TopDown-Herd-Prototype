using System;
using Domain.Animals;
using Domain.Herd;
using Domain.Score;
using Application.World;

namespace Application.Animals
{
    public sealed class AnimalDeliveryService
    {
        private readonly IHerdService _herdService;
        private readonly IScoreService _scoreService;
        private readonly GameplayWorld _world;

        public event Action<AnimalModel> Delivered;

        public AnimalDeliveryService(
            IHerdService herdService,
            IScoreService scoreService,
            GameplayWorld world)
        {
            _herdService = herdService;
            _scoreService = scoreService;
            _world = world;
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
                _scoreService.Add(1);

                Delivered?.Invoke(animal);
            }
        }
    }
}
