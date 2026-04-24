using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "AnimalConfig",
        menuName = "Configs/Animal Config")]
    public class AnimalConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)]
        public float MoveSpeed { get; private set; } = 3f;

        [field: SerializeField, Min(0f)]
        public float FollowDistance { get; private set; } = 0.7f;

        [field: SerializeField, Min(0f)]
        public float CollectRadius { get; private set; } = 1.2f;

        [field: SerializeField, Min(0f)]
        public float PatrolRadius { get; private set; } = 3f;

        [field: SerializeField, Min(0f)]
        public float PatrolPointReachDistance { get; private set; } = 0.15f;
    }
}