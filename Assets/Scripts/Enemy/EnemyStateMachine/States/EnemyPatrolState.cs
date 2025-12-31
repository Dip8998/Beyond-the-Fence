using BTF.Interfaces;
using UnityEngine;

namespace BTF.Enemy
{ 
    public class EnemyPatrolState : IState<EnemyController>
    {
        private EnemyController owner;
        private const float REACH_DISTANCE = 0.2f;

        public void SetOwner(EnemyController owner) => this.owner = owner;

        public void OnStateEnter() { }

        public void Update()
        {
            Vector2 target = owner.GetPatrolTarget();
            Vector2 currentPos = owner.GetPosition();

            Vector2 dir = target - currentPos;

            if(dir.sqrMagnitude <= REACH_DISTANCE * REACH_DISTANCE)
            {
                owner.AdvancePatrolPoint();
                owner.SetVelocity(Vector2.zero);
                return;
            }

            Vector2 velocity = dir.normalized * owner.GetMoveSpeed();
            owner.SetVelocity(velocity);
        }

        public void OnStateExit()
        {
            owner.SetVelocity(Vector2.zero);
        }
    }
}
