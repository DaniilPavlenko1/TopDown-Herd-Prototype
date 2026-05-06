using System;
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

            if (heroConfig != null)
            {
                Debug.Assert(
                    heroConfig.MoveSpeed > 0f,
                    "HeroConfig: MoveSpeed must be > 0.");
            }

            if (animalConfig != null)
            {
                Debug.Assert(
                    animalConfig.MoveSpeed > 0f,
                    "AnimalConfig: MoveSpeed must be > 0.");

                Debug.Assert(
                    animalConfig.PatrolPointReachDistance <= animalConfig.PatrolRadius,
                    "AnimalConfig: PatrolPointReachDistance must be <= PatrolRadius.");
            }

            if (herdConfig != null)
            {
                Debug.Assert(
                    herdConfig.MaxAnimals > 0,
                    "HerdConfig: MaxAnimals must be > 0.");
            }

            if (spawnerConfig == null)
                return;

            Debug.Assert(
                spawnerConfig.SpawnIntervalMin <= spawnerConfig.SpawnIntervalMax,
                "SpawnerConfig: SpawnIntervalMin must be <= SpawnIntervalMax.");

            Debug.Assert(
                spawnerConfig.InitialSpawnCount <= spawnerConfig.MaxAliveAnimals,
                "SpawnerConfig: InitialSpawnCount must be <= MaxAliveAnimals.");
        }

        public static void ThrowIfInvalid(
            HeroConfig heroConfig,
            AnimalConfig animalConfig,
            HerdConfig herdConfig,
            SpawnerConfig spawnerConfig)
        {
            Validate(heroConfig, animalConfig, herdConfig, spawnerConfig);

            if (heroConfig == null)
                throw new InvalidOperationException("HeroConfig is not assigned.");

            if (animalConfig == null)
                throw new InvalidOperationException("AnimalConfig is not assigned.");

            if (herdConfig == null)
                throw new InvalidOperationException("HerdConfig is not assigned.");

            if (spawnerConfig == null)
                throw new InvalidOperationException("SpawnerConfig is not assigned.");

            if (heroConfig.MoveSpeed <= 0f)
                throw new InvalidOperationException("HeroConfig: MoveSpeed must be > 0.");

            if (animalConfig.MoveSpeed <= 0f)
                throw new InvalidOperationException("AnimalConfig: MoveSpeed must be > 0.");

            if (animalConfig.PatrolPointReachDistance > animalConfig.PatrolRadius)
            {
                throw new InvalidOperationException(
                    "AnimalConfig: PatrolPointReachDistance must be <= PatrolRadius.");
            }

            if (herdConfig.MaxAnimals <= 0)
                throw new InvalidOperationException("HerdConfig: MaxAnimals must be > 0.");

            if (spawnerConfig.SpawnIntervalMin > spawnerConfig.SpawnIntervalMax)
            {
                throw new InvalidOperationException(
                    "SpawnerConfig: SpawnIntervalMin must be <= SpawnIntervalMax.");
            }

            if (spawnerConfig.InitialSpawnCount > spawnerConfig.MaxAliveAnimals)
            {
                throw new InvalidOperationException(
                    "SpawnerConfig: InitialSpawnCount must be <= MaxAliveAnimals.");
            }
        }
    }
}
