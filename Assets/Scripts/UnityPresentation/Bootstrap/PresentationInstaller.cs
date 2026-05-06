using Application.Common;
using Application.Animals.Events;
using System;
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
            private readonly AnimalViewRegistry _animalViewRegistry;
            private readonly IDisposable _spawnedSubscription;
            private readonly IDisposable _deliveredSubscription;

            public PresentationRuntimeBindings(
                SceneReferences sceneReferences,
                GameplayContext gameplay,
                AnimalViewRegistry animalViewRegistry)
            {
                _sceneReferences = sceneReferences;
                _animalViewRegistry = animalViewRegistry;

                _sceneReferences.ScoreView.Bind(gameplay.ScoreService);
                _spawnedSubscription = gameplay.EventBus.Subscribe<AnimalSpawnedEvent>(OnAnimalSpawned);
                _deliveredSubscription = gameplay.EventBus.Subscribe<AnimalDeliveredEvent>(OnAnimalDelivered);
            }

            public void Dispose()
            {
                _deliveredSubscription.Dispose();
                _spawnedSubscription.Dispose();
                _sceneReferences.ScoreView.Unbind();
            }

            private void OnAnimalSpawned(AnimalSpawnedEvent eventData)
            {
                _animalViewRegistry.Add(eventData.Animal);
            }

            private void OnAnimalDelivered(AnimalDeliveredEvent eventData)
            {
                _animalViewRegistry.Remove(eventData.Animal);
            }
        }
    }
}
