using Application.Gameplay;
using Configs;
using UnityEngine;
using UnityPresentation.Diagnostics;

namespace UnityPresentation.Bootstrap
{
    public sealed class SceneCompositionRoot : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField] private SceneReferences sceneReferences;

        [Header("Configs")]
        [SerializeField] private HeroConfig heroConfig;
        [SerializeField] private AnimalConfig animalConfig;
        [SerializeField] private HerdConfig herdConfig;
        [SerializeField] private SpawnerConfig spawnerConfig;

        [Header("World Layout")]
        [SerializeField] private float visibleHeight = 10f;
        [SerializeField, Range(0.1f, 0.4f)] private float yardWidthPercent = 0.18f;
        [SerializeField, Range(0.2f, 0.9f)] private float yardHeightPercent = 0.45f;
        [SerializeField] private float horizontalPadding = 1f;
        [SerializeField] private float verticalPadding = 1f;
        [SerializeField] private float gapBetweenSpawnAndYard = 1f;

        private GameplayLifetime _lifetime;

        private void Awake()
        {
            ValidateReferences();

            _lifetime = new GameplayLifetime();

            WorldInstallerResult world = new WorldInstaller().Install(
                sceneReferences,
                visibleHeight,
                yardWidthPercent,
                yardHeightPercent,
                horizontalPadding,
                verticalPadding,
                gapBetweenSpawnAndYard);

            GameplayInstallerResult gameplay = new GameplayInstaller().Install(
                world.GameplayWorld,
                heroConfig,
                animalConfig,
                herdConfig,
                spawnerConfig,
                sceneReferences.MainCamera);

            PresentationInstallerResult presentation = new PresentationInstaller().Install(
                sceneReferences,
                gameplay);

            RegisterGameplay(gameplay);
            RegisterPresentation(presentation);

            _lifetime.Initialize();
        }

        private void Update()
        {
            _lifetime.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _lifetime?.Dispose();
        }

        private void RegisterGameplay(GameplayInstallerResult gameplay)
        {
            _lifetime.AddInitializable(gameplay.SpawnTimerService);
            _lifetime.AddTickable(gameplay.GameplayUpdateService);
            _lifetime.AddTickable(gameplay.SpawnTimerService);

            _lifetime.AddDisposable(gameplay.HeroInputService);
            _lifetime.AddDisposable(gameplay.RuntimeBindings);
        }

        private void RegisterPresentation(PresentationInstallerResult presentation)
        {
            _lifetime.AddTickable(presentation.ViewTickService);
            _lifetime.AddDisposable(presentation.ViewTickService);
            _lifetime.AddDisposable(presentation.RuntimeBindings);
        }

        private void ValidateReferences()
        {
            SceneReferenceValidator.Validate(sceneReferences);

            ConfigValidator.Validate(
                heroConfig,
                animalConfig,
                herdConfig,
                spawnerConfig);
        }
    }
}
