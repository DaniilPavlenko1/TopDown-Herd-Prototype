using System;

namespace Domain.Animals
{
    public readonly struct AnimalId : IEquatable<AnimalId>
    {
        public int Value { get; }

        public AnimalId(int value)
        {
            Value = value;
        }

        public bool Equals(AnimalId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is AnimalId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
