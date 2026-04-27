using System;

namespace Score
{
    public interface IScoreService
    {
        event Action<int> ScoreChanged;

        int Score { get; }

        void AddScore(int amount);
    }
}