using System;

namespace Domain.Common
{
    public readonly struct GameVector2 : IEquatable<GameVector2>
    {
        public float X { get; }
        public float Y { get; }

        public static GameVector2 Zero => new(0f, 0f);

        public GameVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float SqrMagnitude => X * X + Y * Y;

        public float Magnitude => MathF.Sqrt(SqrMagnitude);

        public GameVector2 Normalized
        {
            get
            {
                float magnitude = Magnitude;

                if (magnitude <= 0.0001f)
                    return Zero;

                return new GameVector2(X / magnitude, Y / magnitude);
            }
        }

        public static float Distance(GameVector2 a, GameVector2 b)
        {
            return (a - b).Magnitude;
        }

        public static GameVector2 operator +(GameVector2 a, GameVector2 b)
        {
            return new GameVector2(a.X + b.X, a.Y + b.Y);
        }

        public static GameVector2 operator -(GameVector2 a, GameVector2 b)
        {
            return new GameVector2(a.X - b.X, a.Y - b.Y);
        }

        public static GameVector2 operator *(GameVector2 value, float multiplier)
        {
            return new GameVector2(value.X * multiplier, value.Y * multiplier);
        }

        public bool Equals(GameVector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is GameVector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
