using Configs;
using UnityEngine;

namespace UnityPresentation.Diagnostics
{
    public static class ConfigValidator
    {
        public static void Validate(
            HeroConfig heroConfig,
            AnimalConfig animalConfig,
            HerdConfig herdConfig,
            SpawnerConfig spawnerConfig)
        {
            Debug.Assert(heroConfig != null, "HeroConfig is not assigned.");
            Debug.Assert(animalConfig != null, "AnimalConfig is not assigned.");
            Debug.Assert(herdConfig != null, "HerdConfig is not assigned.");
            Debug.Assert(spawnerConfig != null, "SpawnerConfig is not assigned.");

            if (spawnerConfig == null)
                return;

            Debug.Assert(
                spawnerConfig.SpawnIntervalMin <= spawnerConfig.SpawnIntervalMax,
                "SpawnerConfig: SpawnIntervalMin must be <= SpawnIntervalMax.");
        }
    }
}
