using System;

namespace Domain.Score
{
    public interface IScoreService
    {
        event Action<int> Changed;

        int Value { get; }

        void Add(int amount);
        void Reset();
    }
}
