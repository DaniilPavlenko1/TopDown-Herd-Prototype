namespace Domain.Common
{
    public static class GameMath
    {
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (System.MathF.Abs(target - current) <= maxDelta)
                return target;

            return current + System.MathF.Sign(target - current) * maxDelta;
        }

        public static GameVector2 MoveTowards(
            GameVector2 current,
            GameVector2 target,
            float maxDistanceDelta)
        {
            GameVector2 difference = target - current;
            float distance = difference.Magnitude;

            if (distance <= maxDistanceDelta || distance <= 0.0001f)
                return target;

            return current + difference.Normalized * maxDistanceDelta;
        }
    }
}
