using System.Collections.Generic;
using Domain.Animals;

namespace Domain.Herd
{
    public sealed class HerdService : IHerdService
    {
        private readonly HerdSettings _settings;
        private readonly List<AnimalModel> _animals = new();

        public int Count => _animals.Count;
        public bool IsFull => _animals.Count >= _settings.MaxAnimals;
        public IReadOnlyList<AnimalModel> Animals => _animals;

        public HerdService(HerdSettings settings)
        {
            _settings = settings;
        }

        public bool TryAdd(AnimalModel animal)
        {
            if (animal == null)
                return false;

            if (IsFull)
                return false;

            if (_animals.Contains(animal))
                return false;

            _animals.Add(animal);
            animal.SetStatus(AnimalStatus.Follow);

            return true;
        }

        public bool Remove(AnimalModel animal)
        {
            if (animal == null)
                return false;

            return _animals.Remove(animal);
        }

        public bool Contains(AnimalModel animal)
        {
            return _animals.Contains(animal);
        }

        public int GetIndexOf(AnimalModel animal)
        {
            return _animals.IndexOf(animal);
        }
    }
}
