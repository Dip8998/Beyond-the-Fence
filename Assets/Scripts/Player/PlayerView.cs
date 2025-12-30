using UnityEngine;

namespace BTF.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerView : MonoBehaviour
    {
        private PlayerController controller;
        private Rigidbody2D rb;
        private Animator animator;

        private Vector2 lastMoveDir = Vector2.down;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        public void Bind(PlayerController controller)
        {
            this.controller = controller;
        }

        private void Update()
        {
            controller?.Tick();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (controller == null) return;
            rb.linearVelocity = controller.GetVelocity();
        }

        private void UpdateAnimation()
        {
            Vector2 velocity = controller.GetVelocity();
            bool isMoving = velocity.sqrMagnitude > 0.001f;

            animator.SetBool(IsMoving, isMoving);

            if (isMoving)
            {
                Vector2 dir = velocity.normalized;

                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    lastMoveDir = new Vector2(Mathf.Sign(dir.x), 0);
                else
                    lastMoveDir = new Vector2(0, Mathf.Sign(dir.y));
            }

            animator.SetFloat(MoveX, lastMoveDir.x);
            animator.SetFloat(MoveY, lastMoveDir.y);
        }
    }
}
