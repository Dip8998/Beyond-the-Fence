namespace BTF.Enemy
{
    public class EnemyModel
    {
        public float MoveSpeed { get; }
        public float ChaseSpeed { get; }

        public int MaxHP { get; }
        public int CurrentHP { get; private set; }

        public EnemyModel(float moveSpeed, float chaseSpeed, int maxHP)
        {
            MoveSpeed = moveSpeed;
            ChaseSpeed = chaseSpeed;
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        public void ReduceHP(int damage)
        {
            CurrentHP -= damage;
        }
    }
}
