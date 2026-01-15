using BTF.Interfaces;
using System;
using UnityEngine;

namespace BTF.Enemy
{
    public sealed class EnemyController
    {
        private EnemyModel model;
        private EnemyStateMachine stateMachine;
        private EnemyView view;

        private Transform targetPlayer;

        private Vector2 currentVelocity;
        public event Action OnEnemyDied;

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

        public Vector2 GetPlayerPosition()
        {
            return targetPlayer != null ? (Vector2)targetPlayer.position : GetPosition();
        }

        public bool IsChasing() => stateMachine.IsInState(EnemyStates.Chase);

        public void TakeDamage(int damage)
        {
            model.ReduceHP(damage);

            if(model.CurrentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnEnemyDied?.Invoke();
            view?.OnDeath();
        }

        public void OnPlayerDetected(Transform target)
        {
            targetPlayer = target;
            Debug.Log("Player is in Range");
            stateMachine.ChangeState(EnemyStates.Chase);
        }

        public void OnPlayerLost()
        {
            targetPlayer = null;
            Debug.Log("Player is Out of range");
            stateMachine.ChangeState(EnemyStates.Patrol);
        }
    }
}
