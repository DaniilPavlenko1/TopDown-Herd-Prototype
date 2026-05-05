using System.Collections.Generic;
using Domain.Animals;
using Domain.Animals.States;
using Domain.Common;
using Domain.Herd;

namespace Application.Animals
{
    public sealed class AnimalCollectionService
    {
        private readonly IHerdService _herdService;
        private readonly AnimalSettings _settings;

        public AnimalCollectionService(
            IHerdService herdService,
            AnimalSettings settings)
        {
            _herdService = herdService;
            _settings = settings;
        }

        public void TryCollectNearbyAnimals(
            GameVector2 heroPosition,
            IReadOnlyList<AnimalModel> animals,
            IAnimalState followState)
        {
            if (_herdService.IsFull)
                return;

            for (int i = 0; i < animals.Count; i++)
            {
                AnimalModel animal = animals[i];

                if (animal.Status != AnimalStatus.Patrol)
                    continue;

                float distance = GameVector2.Distance(heroPosition, animal.Position);

                if (distance > _settings.CollectRadius)
                    continue;

                if (_herdService.TryAdd(animal))
                {
                    animal.SetState(followState);
                }

                if (_herdService.IsFull)
                    return;
            }
        }
    }
}
