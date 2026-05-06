using Application.World;
using UnityPresentation.World;

namespace UnityPresentation.Bootstrap
{
    public sealed class WorldInstaller
    {
        public WorldContext Install(
            SceneReferences sceneReferences,
            float visibleHeight,
            float yardWidthPercent,
            float yardHeightPercent,
            float horizontalPadding,
            float verticalPadding,
            float gapBetweenSpawnAndYard)
        {
            var layoutSettings = new WorldLayoutSettings(
                visibleHeight,
                sceneReferences.CameraView.AspectRatio,
                yardWidthPercent,
                yardHeightPercent,
                horizontalPadding,
                verticalPadding,
                gapBetweenSpawnAndYard);

            var layoutService = new WorldLayoutService();
            WorldLayout layout = layoutService.Calculate(layoutSettings);
            sceneReferences.GameplayGizmos?.SetLayout(layout);

            var gameplayWorld = new GameplayWorld(
                layout.WorldBounds,
                layout.SpawnBounds,
                layout.YardBounds);

            var layoutApplier = new WorldLayoutApplier(
                sceneReferences.CameraView,
                sceneReferences.GroundView,
                sceneReferences.YardView,
                sceneReferences.SpawnAreaView);

            layoutApplier.Apply(layout);

            return new WorldContext(gameplayWorld, layout);
        }
    }
}
