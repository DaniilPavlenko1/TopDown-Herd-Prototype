using Application.Common;

namespace UnityPresentation.Bindings
{
    public sealed class ViewTickService : ITickable, IDisposableService
    {
        private readonly HeroViewBinder _heroViewBinder;
        private readonly AnimalViewRegistry _animalViewRegistry;

        public ViewTickService(
            HeroViewBinder heroViewBinder,
            AnimalViewRegistry animalViewRegistry)
        {
            _heroViewBinder = heroViewBinder;
            _animalViewRegistry = animalViewRegistry;
        }

        public void Tick(float deltaTime)
        {
            _heroViewBinder.Tick();
            _animalViewRegistry.Tick();
        }

        public void Dispose()
        {
            _heroViewBinder.Dispose();
            _animalViewRegistry.Clear();
        }
    }
}
