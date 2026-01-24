using UnityEngine;

namespace BTF.Guidance
{
    public sealed class WorldGuideArrow : MonoBehaviour
    {
        [SerializeField] private float bobHeight = 0.2f;
        [SerializeField] private float bobSpeed = 3f;

        private Transform target;
        private Vector3 basePos;

        private void Awake()
        {
            basePos = transform.position;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 dir = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = basePos + Vector3.up * yOffset;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}