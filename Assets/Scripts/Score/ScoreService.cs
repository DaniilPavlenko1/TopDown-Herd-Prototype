using System;

namespace Score
{
    public class ScoreService : IScoreService
    {
        public event Action<int> ScoreChanged;

        public int Score { get; private set; }

        public void AddScore(int amount)
        {
            if (amount <= 0)
                return;

            Score += amount;
            ScoreChanged?.Invoke(Score);
        }
    }
}