using System;

namespace Domain.Score
{
    public sealed class ScoreModel
    {
        public event Action<int> Changed;

        public int Value { get; private set; }

        public void Add(int amount)
        {
            if (amount <= 0)
                return;

            Value += amount;
            Changed?.Invoke(Value);
        }

        public void Reset()
        {
            Value = 0;
            Changed?.Invoke(Value);
        }
    }
}
