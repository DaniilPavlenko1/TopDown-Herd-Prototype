using System.Collections.Generic;
using Domain.Animals;

namespace Domain.Herd
{
    public interface IHerdService
    {
        int Count { get; }
        bool IsFull { get; }

        IReadOnlyList<AnimalModel> Animals { get; }

        bool TryAdd(AnimalModel animal);
        bool Remove(AnimalModel animal);
        bool Contains(AnimalModel animal);
        int GetIndexOf(AnimalModel animal);
    }
}
