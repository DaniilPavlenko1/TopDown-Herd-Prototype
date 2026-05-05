using Domain.Hero;

namespace Application.Input
{
    public sealed class HeroInputService
    {
        private readonly HeroModel _hero;
        private readonly IPlayerInput _input;

        public HeroInputService(
            HeroModel hero,
            IPlayerInput input)
        {
            _hero = hero;
            _input = input;

            _input.MoveCommand += OnMoveCommand;
        }

        public void Dispose()
        {
            _input.MoveCommand -= OnMoveCommand;
        }

        private void OnMoveCommand(Domain.Common.GameVector2 position)
        {
            _hero.SetTarget(position);
        }
    }
}
