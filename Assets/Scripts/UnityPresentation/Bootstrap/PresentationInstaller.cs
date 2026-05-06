using Application.Common;
using Domain.Animals;
using UnityPresentation.Bindings;
using UnityPresentation.Pooling;

namespace UnityPresentation.Bootstrap
{
    public sealed class PresentationInstaller
    {
        public PresentationContext Install(
            SceneReferences sceneReferences,
            GameplayContext gameplay)
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

            return new PresentationContext(
                heroViewBinder,
                animalViewRegistry,
                viewTickService,
                runtimeBindings);
        }

        private sealed class PresentationRuntimeBindings : IDisposableService
        {
            private readonly SceneReferences _sceneReferences;
            private readonly GameplayContext _gameplay;
            private readonly AnimalViewRegistry _animalViewRegistry;

            public PresentationRuntimeBindings(
                SceneReferences sceneReferences,
                GameplayContext gameplay,
                AnimalViewRegistry animalViewRegistry)
            {
                _sceneReferences = sceneReferences;
                _gameplay = gameplay;
                _animalViewRegistry = animalViewRegistry;

                _sceneReferences.ScoreView.Bind(_gameplay.ScoreService);
                _gameplay.SpawnService.Spawned += OnAnimalSpawned;
                _gameplay.DeliveryService.Delivered += OnAnimalDelivered;
            }

            public void Dispose()
            {
                _gameplay.SpawnService.Spawned -= OnAnimalSpawned;
                _gameplay.DeliveryService.Delivered -= OnAnimalDelivered;
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
}
