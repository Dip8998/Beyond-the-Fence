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

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }

        public void Bind(PlayerController controller) => this.controller = controller;

        private void Update()
        {
            controller?.Tick();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = controller.GetVelocity();
        }

        private void UpdateAnimation()
        {
            Vector2 velocity = controller.GetVelocity();
            bool isMoving = velocity.sqrMagnitude > 0.01f;

            animator.SetBool(IsMoving, isMoving);

            if (isMoving)
            {
                lastMoveDir = velocity.normalized;
            }

            animator.SetFloat(MoveX, lastMoveDir.x);
            animator.SetFloat(MoveY, lastMoveDir.y);
        }
    }
}
