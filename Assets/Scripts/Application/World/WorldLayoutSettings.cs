namespace Application.World
{
    public readonly struct WorldLayoutSettings
    {
        public float VisibleHeight { get; }
        public float AspectRatio { get; }

        public float YardWidthPercent { get; }
        public float YardHeightPercent { get; }

        public float HorizontalPadding { get; }
        public float VerticalPadding { get; }
        public float GapBetweenSpawnAndYard { get; }

        public WorldLayoutSettings(
            float visibleHeight,
            float aspectRatio,
            float yardWidthPercent,
            float yardHeightPercent,
            float horizontalPadding,
            float verticalPadding,
            float gapBetweenSpawnAndYard)
        {
            VisibleHeight = visibleHeight;
            AspectRatio = aspectRatio;
            YardWidthPercent = yardWidthPercent;
            YardHeightPercent = yardHeightPercent;
            HorizontalPadding = horizontalPadding;
            VerticalPadding = verticalPadding;
            GapBetweenSpawnAndYard = gapBetweenSpawnAndYard;
        }
    }
}
