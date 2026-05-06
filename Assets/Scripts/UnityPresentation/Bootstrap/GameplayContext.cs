using Application.Animals;
using Application.Common;
using Application.Gameplay;
using Application.Input;
using Domain.Hero;
using Domain.Score;

namespace UnityPresentation.Bootstrap
{
    public readonly struct GameplayContext
    {
        public HeroModel Hero { get; }
        public IScoreService ScoreService { get; }
        public GameplayUpdateService UpdateService { get; }
        public AnimalSpawnService SpawnService { get; }
        public AnimalDeliveryService DeliveryService { get; }
        public AnimalSpawnTimerService SpawnTimerService { get; }
        public HeroInputService HeroInputService { get; }
        public IDisposableService RuntimeBindings { get; }

        public GameplayContext(
            HeroModel hero,
            IScoreService scoreService,
            GameplayUpdateService updateService,
            AnimalSpawnService spawnService,
            AnimalDeliveryService deliveryService,
            AnimalSpawnTimerService spawnTimerService,
            HeroInputService heroInputService,
            IDisposableService runtimeBindings)
        {
            Hero = hero;
            ScoreService = scoreService;
            UpdateService = updateService;
            SpawnService = spawnService;
            DeliveryService = deliveryService;
            SpawnTimerService = spawnTimerService;
            HeroInputService = heroInputService;
            RuntimeBindings = runtimeBindings;
        }
    }
}
