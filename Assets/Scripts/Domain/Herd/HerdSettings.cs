namespace Domain.Herd
{
    public readonly struct HerdSettings
    {
        public int MaxAnimals { get; }

        public HerdSettings(int maxAnimals)
        {
            MaxAnimals = maxAnimals;
        }
    }
}
