using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "SpawnerConfig",
        menuName = "Configs/Spawner Config")]
    public class SpawnerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)]
        public float SpawnIntervalMin { get; private set; } = 2f;

        [field: SerializeField, Min(0.1f)]
        public float SpawnIntervalMax { get; private set; } = 5f;

        [field: SerializeField, Min(0)]
        public int InitialSpawnCount { get; private set; } = 5;

        [field: SerializeField, Min(1)]
        public int MaxAliveAnimals { get; private set; } = 15;
    }
}