using BTF.Interfaces;
using System;
using UnityEngine;

namespace BTF.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class EnemyView : MonoBehaviour , IDamageable
    {
        [SerializeField] private Transform[] patrolPoints;

        private EnemyController controller;
        private Rigidbody2D rb;
        [NonSerialized]
        protected Animator animator;
        private int currentPatrolIndex;

        private Vector2 lastMoveDir = Vector2.down;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            rb.freezeRotation = true;
            rb.gravityScale = 0f;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            Debug.Assert(patrolPoints.Length > 0,
                "[EnemyView] Patrol points not assigned");
        }

        public void Bind(EnemyController controller)
        {
            this.controller = controller;
            controller?.Bind(this);
        }

        private void Update()
        {
            controller?.Tick();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if(controller == null) return;
            rb.linearVelocity = controller.GetVelocity();
        }

        private void UpdateAnimation()
        {
            Vector2 velocity = controller.GetVelocity();
            bool isMoving = velocity.sqrMagnitude > 0.001f;

            animator.SetBool(IsMoving, isMoving);

            if(isMoving )
            {
                lastMoveDir = velocity.normalized;
                animator.SetFloat(MoveX, lastMoveDir.x);
                animator.SetFloat (MoveY, lastMoveDir.y);
            }

            animator.speed = controller.IsChasing() ? 1.4f : 1f;
        }

        public Vector2 GetCurrentPatrolTarget()
        {
            if(patrolPoints == null ||  patrolPoints.Length == 0) return transform.position;

            return patrolPoints[currentPatrolIndex].position;
        }

        public void AdvancePatrolPoint()
        {
            if(patrolPoints == null || patrolPoints.Length == 0) return;

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        public void TakeDamage(int damage) => controller?.TakeDamage(damage);

        public virtual void OnDeath()
        {
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
