namespace BTF.Enemy
{
    public interface IBoss
    {
        EnemyController Controller { get; }
        void TriggerAttack();
    }
}
