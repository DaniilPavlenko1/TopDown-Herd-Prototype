namespace Domain.Movement
{
    public readonly struct MovementSettings
    {
        public float Speed { get; }
        public float StopDistance { get; }

        public MovementSettings(float speed, float stopDistance)
        {
            Speed = speed;
            StopDistance = stopDistance;
        }
    }
}
