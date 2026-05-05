namespace Domain.Common
{
    public readonly struct GameBounds
    {
        public GameVector2 Center { get; }
        public GameVector2 Size { get; }

        public float MinX => Center.X - Size.X * 0.5f;
        public float MaxX => Center.X + Size.X * 0.5f;
        public float MinY => Center.Y - Size.Y * 0.5f;
        public float MaxY => Center.Y + Size.Y * 0.5f;

        public GameBounds(GameVector2 center, GameVector2 size)
        {
            Center = center;
            Size = size;
        }

        public bool Contains(GameVector2 position)
        {
            return position.X >= MinX &&
                   position.X <= MaxX &&
                   position.Y >= MinY &&
                   position.Y <= MaxY;
        }

        public GameVector2 Clamp(GameVector2 position)
        {
            return new GameVector2(
                GameMath.Clamp(position.X, MinX, MaxX),
                GameMath.Clamp(position.Y, MinY, MaxY));
        }
    }
}
