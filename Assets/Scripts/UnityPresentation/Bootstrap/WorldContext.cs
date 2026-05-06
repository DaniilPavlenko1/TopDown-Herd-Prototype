using Application.World;

namespace UnityPresentation.Bootstrap
{
    public readonly struct WorldContext
    {
        public GameplayWorld GameplayWorld { get; }
        public WorldLayout Layout { get; }

        public WorldContext(GameplayWorld gameplayWorld, WorldLayout layout)
        {
            GameplayWorld = gameplayWorld;
            Layout = layout;
        }
    }
}
