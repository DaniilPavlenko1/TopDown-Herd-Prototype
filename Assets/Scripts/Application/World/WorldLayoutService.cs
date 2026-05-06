using Domain.Common;

namespace Application.World
{
    public sealed class WorldLayoutService
    {
        public WorldLayout Calculate(WorldLayoutSettings settings)
        {
            float worldHeight = settings.VisibleHeight;
            float worldWidth = worldHeight * settings.AspectRatio;

            var worldBounds = new GameBounds(
                GameVector2.Zero,
                new GameVector2(worldWidth, worldHeight));

            float yardWidth = worldWidth * settings.YardWidthPercent;
            float yardHeight = worldHeight * settings.YardHeightPercent;

            float yardCenterX =
                worldBounds.MaxX -
                settings.HorizontalPadding -
                yardWidth * 0.5f;

            var yardBounds = new GameBounds(
                new GameVector2(yardCenterX, 0f),
                new GameVector2(yardWidth, yardHeight));

            float spawnMinX = worldBounds.MinX + settings.HorizontalPadding;
            float spawnMaxX = yardBounds.MinX - settings.GapBetweenSpawnAndYard;

            float spawnMinY = worldBounds.MinY + settings.VerticalPadding;
            float spawnMaxY = worldBounds.MaxY - settings.VerticalPadding;

            float spawnWidth = GameMath.Clamp(spawnMaxX - spawnMinX, 1f, worldWidth);
            float spawnHeight = GameMath.Clamp(spawnMaxY - spawnMinY, 1f, worldHeight);

            var spawnBounds = new GameBounds(
                new GameVector2(
                    spawnMinX + spawnWidth * 0.5f,
                    spawnMinY + spawnHeight * 0.5f),
                new GameVector2(spawnWidth, spawnHeight));

            return new WorldLayout(
                worldBounds,
                spawnBounds,
                yardBounds);
        }
    }
}
