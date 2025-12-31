namespace BTF.Enemy
{
    public class EnemyModel
    {
        public float MoveSpeed { get; }
        public float ChaseSpeed { get; }

        public EnemyModel(float moveSpeed, float chaseSpeed)
        {
            MoveSpeed = moveSpeed;
            ChaseSpeed = chaseSpeed;
        }
    }
}
