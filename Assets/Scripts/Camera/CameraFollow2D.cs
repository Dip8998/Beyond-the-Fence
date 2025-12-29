using UnityEngine;

namespace BTF.Camera
{
    public sealed class CameraFollow2D : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private float smoothTime = 0.15f;
        [SerializeField] private Vector3 offset;

        private Vector3 velocity;
        private Transform target;

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 targetPos = target.position + offset;
            targetPos.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime
            );
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}
