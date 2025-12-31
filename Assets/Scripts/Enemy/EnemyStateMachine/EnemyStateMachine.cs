using BTF.StateMachine;

namespace BTF.Enemy
{
    public class EnemyStateMachine : GenericStateMachine<EnemyController>
    {
        public EnemyStateMachine(EnemyController owner) : base(owner)
        {
            States.Add(EnemyStates.Patrol, new EnemyPatrolState());
            //States.Add(EnemyStates.Chase, new EnemyChaseState());

            SetOwner();

            ChangeState(EnemyStates.Patrol);
        }
    }
}
