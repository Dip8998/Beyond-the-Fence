using BTF.Interfaces;
using UnityEngine;

namespace BTF.Enemy
{ 
    public class EnemyPatrolState : IState<EnemyController>
    {
        private EnemyController owner;
        private const float REACH_DISTANCE = 0.2f;
        private const float WAIT_DURATION = 1.5f;

        private bool isWaiting;
        private float waitTimer;

        public void SetOwner(EnemyController owner) => this.owner = owner;

        public void OnStateEnter()
        {
            isWaiting = false;
            waitTimer = 0;
        }

        public void Update()
        {
            Vector2 target = owner.GetPatrolTarget();
            Vector2 currentPos = owner.GetPosition();

            float sqrDistance = (target - currentPos).sqrMagnitude;

            if (sqrDistance <= REACH_DISTANCE * REACH_DISTANCE)
            {
                if (!isWaiting)
                {
                    isWaiting = true;
                    waitTimer = 0;
                    owner.SetVelocity(Vector2.zero);
                    return;
                }

                waitTimer += Time.deltaTime;

                if(waitTimer > WAIT_DURATION)
                {
                    isWaiting = false;
                    waitTimer = 0;
                    owner.AdvancePatrolPoint();
                }
                return;
            }

            Vector2 dir = (target - currentPos).normalized; 
            Vector2 velocity = dir * owner.GetMoveSpeed();
            owner.SetVelocity(velocity);
        }

        public void OnStateExit()
        {
            owner.SetVelocity(Vector2.zero);
        }
    }
}
