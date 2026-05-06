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
        public IEventBus EventBus { get; }
        public GameplayUpdateService UpdateService { get; }
        public AnimalSpawnTimerService SpawnTimerService { get; }
        public HeroInputService HeroInputService { get; }
        public IDisposableService RuntimeBindings { get; }

        public GameplayContext(
            HeroModel hero,
            IScoreService scoreService,
            IEventBus eventBus,
            GameplayUpdateService updateService,
            AnimalSpawnTimerService spawnTimerService,
            HeroInputService heroInputService,
            IDisposableService runtimeBindings)
        {
            Hero = hero;
            ScoreService = scoreService;
            EventBus = eventBus;
            UpdateService = updateService;
            SpawnTimerService = spawnTimerService;
            HeroInputService = heroInputService;
            RuntimeBindings = runtimeBindings;
        }
    }
}
