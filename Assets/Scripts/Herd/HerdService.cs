using System.Collections.Generic;
using Animals;
using Configs;

namespace Herd
{
    public class HerdService : IHerdService
    {
        private readonly HerdConfig _config;
        private readonly List<AnimalController> _animals = new();

        public bool IsFull => _animals.Count >= _config.MaxAnimals;
        public int Count => _animals.Count;

        public HerdService(HerdConfig config)
        {
            _config = config;
        }

        public bool TryAddAnimal(AnimalController animal)
        {
            if (animal == null)
                return false;

            if (IsFull)
                return false;

            if (_animals.Contains(animal))
                return false;

            _animals.Add(animal);
            return true;
        }

        public void RemoveAnimal(AnimalController animal)
        {
            _animals.Remove(animal);
        }

        public bool Contains(AnimalController animal)
        {
            return _animals.Contains(animal);
        }

        public IReadOnlyList<AnimalController> GetAnimals()
        {
            return _animals;
        }
    }
}