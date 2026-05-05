namespace Domain.Animals
{
    public readonly struct AnimalSettings
    {
        public float MoveSpeed { get; }
        public float StopDistance { get; }
        public float FollowDistance { get; }
        public float CollectRadius { get; }
        public float PatrolRadius { get; }
        public float PatrolPointReachDistance { get; }

        public AnimalSettings(
            float moveSpeed,
            float stopDistance,
            float followDistance,
            float collectRadius,
            float patrolRadius,
            float patrolPointReachDistance)
        {
            MoveSpeed = moveSpeed;
            StopDistance = stopDistance;
            FollowDistance = followDistance;
            CollectRadius = collectRadius;
            PatrolRadius = patrolRadius;
            PatrolPointReachDistance = patrolPointReachDistance;
        }
    }
}
