using BTF.Interfaces;
using UnityEngine;

namespace BTF.Enemy
{
    public class EnemyChaseState : IState<EnemyController>
    {
        private EnemyController owner;

        private const float STOP_DISTANCE = 1f;

        public void SetOwner(EnemyController owner) => this.owner = owner;

        public void OnStateEnter() { }

        public void Update()
        {
            Vector2 playerPos = owner.GetPlayerPosition();
            Vector2 enemyPos = owner.GetPosition();

            Vector2 dir = playerPos - enemyPos;

            if(dir.sqrMagnitude <= STOP_DISTANCE * STOP_DISTANCE)
            {
                owner.SetVelocity(Vector2.zero);
            }
            else
            {
                Vector2 velocity = dir.normalized * owner.GetChaseSpeed();
                owner.SetVelocity(velocity);
            }
        }

        public void OnStateExit() { }
    }
}
