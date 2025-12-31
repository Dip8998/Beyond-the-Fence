using UnityEngine;

namespace BTF.Enemy
{
    public sealed class EnemyController
    {
        private EnemyModel model;
        private EnemyStateMachine stateMachine;
        private EnemyView view;

        private bool isPlayerInRange;
        private Transform targetPlayer;

        private Vector2 currentVelocity;

        public EnemyController(EnemyModel model)
        {
            this.model = model;
            stateMachine = new EnemyStateMachine(this);
        }

        public void Tick() => stateMachine?.Update();

        public void Bind(EnemyView view) => this.view = view;

        public void SetVelocity(Vector2 velocity) => currentVelocity = velocity;

        public float GetMoveSpeed() => model.MoveSpeed;

        public float GetChaseSpeed() => model.ChaseSpeed;

        public Vector2 GetPatrolTarget() => view.GetCurrentPatrolTarget();

        public void AdvancePatrolPoint() => view.AdvancePatrolPoint();

        public Vector2 GetVelocity() => currentVelocity;

        public Vector2 GetPosition() => view.transform.position;

        public Vector2 GetPlayerPosition() => targetPlayer.position;

        public void OnPlayerDetected(Transform target)
        {
            isPlayerInRange = true;
            targetPlayer = target;
            Debug.Log("Player is in Range");
            stateMachine.ChangeState(EnemyStates.Chase);
        }

        public void OnPlayerLost()
        {
            isPlayerInRange = false;
            targetPlayer = null;
            Debug.Log("Player is Out of range");
            stateMachine.ChangeState(EnemyStates.Patrol);
        }
    }
}
