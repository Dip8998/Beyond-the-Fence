using UnityEngine;
using BTF.Player;

namespace BTF.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class BossAttackHitbox : MonoBehaviour
    {
        [SerializeField] private int damage = 2;
        [SerializeField] private LayerMask playerLayer;

        private Collider2D hitbox;
        private readonly Collider2D[] results = new Collider2D[1];

        private void Awake()
        {
            hitbox = GetComponent<Collider2D>();

            if (hitbox == null)
            {
                Debug.LogError("[BossAttackHitbox] Collider2D missing!", this);
                return;
            }

            hitbox.enabled = false;
        }

        public void Enable()
        {
            if (hitbox != null)
                hitbox.enabled = true;
        }

        public void Disable()
        {
            if (hitbox != null)
                hitbox.enabled = false;
        }

        public void ApplyDamage()
        {
            if (hitbox == null) return;

            ContactFilter2D filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = playerLayer
            };

            int count = hitbox.Overlap(filter, results);

            if (count == 0)
                return;

            if (results[0].TryGetComponent<PlayerView>(out var view))
            {
                if (!view.Controller.IsInvincible)
                {
                    view.Controller.TakeDamage(damage);
                }
            }
        }
    }
}
