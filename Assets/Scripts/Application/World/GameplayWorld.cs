using Domain.Common;

namespace Application.World
{
    public sealed class GameplayWorld
    {
        public GameBounds WorldBounds { get; private set; }
        public GameBounds SpawnBounds { get; private set; }
        public GameBounds YardBounds { get; private set; }

        public GameplayWorld(
            GameBounds worldBounds,
            GameBounds spawnBounds,
            GameBounds yardBounds)
        {
            WorldBounds = worldBounds;
            SpawnBounds = spawnBounds;
            YardBounds = yardBounds;
        }

        public void SetLayout(
            GameBounds worldBounds,
            GameBounds spawnBounds,
            GameBounds yardBounds)
        {
            WorldBounds = worldBounds;
            SpawnBounds = spawnBounds;
            YardBounds = yardBounds;
        }
    }
}
