using Configs;
using Domain.Animals;
using Domain.Herd;
using Domain.Movement;

namespace UnityPresentation.Bootstrap
{
    public static class ConfigMapper
    {
        public static AnimalSettings ToAnimalSettings(AnimalConfig config)
        {
            return new AnimalSettings(
                config.MoveSpeed,
                0.05f,
                config.FollowDistance,
                config.CollectRadius,
                config.PatrolRadius,
                config.PatrolPointReachDistance);
        }

        public static MovementSettings ToHeroMovementSettings(HeroConfig config)
        {
            return new MovementSettings(
                config.MoveSpeed,
                0.05f);
        }

        public static MovementSettings ToAnimalMovementSettings(AnimalConfig config)
        {
            return new MovementSettings(
                config.MoveSpeed,
                0.05f);
        }

        public static HerdSettings ToHerdSettings(HerdConfig config)
        {
            return new HerdSettings(config.MaxAnimals);
        }
    }
}
