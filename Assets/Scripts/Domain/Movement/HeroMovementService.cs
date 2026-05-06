using Domain.Hero;

namespace Domain.Movement
{
    public sealed class HeroMovementService
    {
        private readonly MovementService _movementService;
        private readonly MovementSettings _settings;

        public HeroMovementService(
            MovementService movementService,
            MovementSettings settings)
        {
            _movementService = movementService;
            _settings = settings;
        }

        public void Tick(HeroModel hero, float deltaTime)
        {
            if (!hero.HasTarget)
                return;

            var newPosition = _movementService.MoveTowards(
                hero.Position,
                hero.TargetPosition,
                _settings,
                deltaTime,
                out bool reached);

            hero.SetPosition(newPosition);

            if (reached)
                hero.ClearTarget();
        }
    }
}
