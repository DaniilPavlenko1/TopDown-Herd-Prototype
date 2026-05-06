using Application.World;

namespace UnityPresentation.Bootstrap
{
    public readonly struct WorldContext
    {
        public GameplayWorld GameplayWorld { get; }

        public WorldContext(GameplayWorld gameplayWorld)
        {
            GameplayWorld = gameplayWorld;
        }
    }
}
