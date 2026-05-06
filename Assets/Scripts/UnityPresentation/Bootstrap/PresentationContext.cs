using Application.Common;
using UnityPresentation.Bindings;

namespace UnityPresentation.Bootstrap
{
    public readonly struct PresentationContext
    {
        public HeroViewBinder HeroBinder { get; }
        public AnimalViewRegistry AnimalRegistry { get; }
        public ViewTickService ViewTickService { get; }
        public IDisposableService RuntimeBindings { get; }

        public PresentationContext(
            HeroViewBinder heroBinder,
            AnimalViewRegistry animalRegistry,
            ViewTickService viewTickService,
            IDisposableService runtimeBindings)
        {
            HeroBinder = heroBinder;
            AnimalRegistry = animalRegistry;
            ViewTickService = viewTickService;
            RuntimeBindings = runtimeBindings;
        }
    }
}
