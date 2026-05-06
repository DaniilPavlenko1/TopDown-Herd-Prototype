using Domain.Common;

namespace Application.World
{
    public readonly struct WorldLayout
    {
        public GameBounds WorldBounds { get; }
        public GameBounds SpawnBounds { get; }
        public GameBounds YardBounds { get; }

        public WorldLayout(
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
