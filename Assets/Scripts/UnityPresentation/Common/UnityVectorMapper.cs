using Domain.Common;
using UnityEngine;

namespace UnityPresentation.Common
{
    public static class UnityVectorMapper
    {
        public static Vector3 ToVector3(GameVector2 value, float z = 0f)
        {
            return new Vector3(value.X, value.Y, z);
        }

        public static GameVector2 ToGameVector2(Vector3 value)
        {
            return new GameVector2(value.x, value.y);
        }

        public static GameVector2 ToGameVector2(Vector2 value)
        {
            return new GameVector2(value.x, value.y);
        }
    }
}
