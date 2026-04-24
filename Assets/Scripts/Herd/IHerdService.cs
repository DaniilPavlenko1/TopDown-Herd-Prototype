using System.Collections.Generic;
using Animals;

namespace Herd
{
    public interface IHerdService
    {
        bool IsFull { get; }
        int Count { get; }

        bool TryAddAnimal(AnimalController animal);
        void RemoveAnimal(AnimalController animal);
        bool Contains(AnimalController animal);
        IReadOnlyList<AnimalController> GetAnimals();
    }
}