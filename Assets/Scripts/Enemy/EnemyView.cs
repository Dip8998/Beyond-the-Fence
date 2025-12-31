using UnityEngine;

namespace BTF.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;

        private EnemyController controller;
        private Rigidbody2D rb;
        private int currentPatrolIndex;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
        }

        private void FixedUpdate()
        {
            if(controller == null) return;
            rb.linearVelocity = controller.GetVelocity();
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
    }
}
