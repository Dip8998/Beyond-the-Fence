using UnityEngine;

namespace BTF.Camera
{
    public sealed class CameraFollow2D : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new(0, 0, -10);
        [SerializeField] private float smoothTime = 0.15f;

        private Transform target;
        private Rigidbody2D targetRb;
        private Vector3 velocity;

        public void SetTarget(Transform target)
        {
            this.target = target;

            if (target.TryGetComponent(out Rigidbody2D rb))
                targetRb = rb;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 targetPos =
                (targetRb != null
                    ? (Vector3)targetRb.position
                    : target.position)
                + offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime
            );
        }
    }
}
