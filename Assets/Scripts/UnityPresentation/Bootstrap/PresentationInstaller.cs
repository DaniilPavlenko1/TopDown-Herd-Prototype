using Application.Common;
using Domain.Animals;
using UnityPresentation.Bindings;
using UnityPresentation.Pooling;

namespace UnityPresentation.Bootstrap
{
    public sealed class PresentationInstaller
    {
        public PresentationInstallerResult Install(
            SceneReferences sceneReferences,
            GameplayInstallerResult gameplay)
        {
            var animalViewPool = new AnimalViewPool(
                sceneReferences.AnimalPrefab,
                sceneReferences.AnimalsContainer);

            var animalViewRegistry = new AnimalViewRegistry(animalViewPool);
            var heroViewBinder = new HeroViewBinder(
                gameplay.Hero,
                sceneReferences.HeroView);

            var viewTickService = new ViewTickService(
                heroViewBinder,
                animalViewRegistry);

            IDisposableService runtimeBindings = new PresentationRuntimeBindings(
                sceneReferences,
                gameplay,
                animalViewRegistry);

            return new PresentationInstallerResult(
                viewTickService,
                runtimeBindings);
        }

        private sealed class PresentationRuntimeBindings : IDisposableService
        {
            private readonly SceneReferences _sceneReferences;
            private readonly GameplayInstallerResult _gameplay;
            private readonly AnimalViewRegistry _animalViewRegistry;

            public PresentationRuntimeBindings(
                SceneReferences sceneReferences,
                GameplayInstallerResult gameplay,
                AnimalViewRegistry animalViewRegistry)
            {
                _sceneReferences = sceneReferences;
                _gameplay = gameplay;
                _animalViewRegistry = animalViewRegistry;

                _sceneReferences.ScoreView.Bind(_gameplay.ScoreService);
                _gameplay.AnimalSpawnService.Spawned += OnAnimalSpawned;
                _gameplay.AnimalDeliveryService.Delivered += OnAnimalDelivered;
            }

            public void Dispose()
            {
                _gameplay.AnimalSpawnService.Spawned -= OnAnimalSpawned;
                _gameplay.AnimalDeliveryService.Delivered -= OnAnimalDelivered;
                _sceneReferences.ScoreView.Unbind();
            }

            private void OnAnimalSpawned(AnimalModel animal)
            {
                _animalViewRegistry.Add(animal);
            }

            private void OnAnimalDelivered(AnimalModel animal)
            {
                _animalViewRegistry.Remove(animal);
            }
        }
    }

    public sealed class PresentationInstallerResult
    {
        public ViewTickService ViewTickService { get; }
        public IDisposableService RuntimeBindings { get; }

        public PresentationInstallerResult(
            ViewTickService viewTickService,
            IDisposableService runtimeBindings)
        {
            ViewTickService = viewTickService;
            RuntimeBindings = runtimeBindings;
        }
    }
}
