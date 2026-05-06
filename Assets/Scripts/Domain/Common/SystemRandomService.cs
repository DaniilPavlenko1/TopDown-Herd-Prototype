using System;

namespace Domain.Common
{
    public sealed class SystemRandomService : IRandomService
    {
        private readonly Random _random;

        public SystemRandomService(int? seed = null)
        {
            _random = seed.HasValue
                ? new Random(seed.Value)
                : new Random();
        }

        public float Range01()
        {
            return (float)_random.NextDouble();
        }

        public float Range(float min, float max)
        {
            return min + (max - min) * Range01();
        }
    }
}
