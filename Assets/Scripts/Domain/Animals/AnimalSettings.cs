namespace Domain.Animals
{
    public readonly struct AnimalSettings
    {
        public float MoveSpeed { get; }
        public float FollowDistance { get; }
        public float CollectRadius { get; }
        public float PatrolRadius { get; }
        public float PatrolPointReachDistance { get; }

        public AnimalSettings(
            float moveSpeed,
            float followDistance,
            float collectRadius,
            float patrolRadius,
            float patrolPointReachDistance)
        {
            MoveSpeed = moveSpeed;
            FollowDistance = followDistance;
            CollectRadius = collectRadius;
            PatrolRadius = patrolRadius;
            PatrolPointReachDistance = patrolPointReachDistance;
        }
    }
}
