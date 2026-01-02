using BTF.Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace BTF.Player
{
    public sealed class PlayerAttackHitbox : MonoBehaviour
    {
        [SerializeField] private int damage = 2;
        [SerializeField] private Collider2D hitboxCollider;
        [SerializeField] private LayerMask enemyLayer;

        private bool hasHit;
        private readonly List<Collider2D> results = new();

        private void OnEnable()
        {
            hasHit = false;
            CheckInitialOverlap();
        }

        private void CheckInitialOverlap()
        {
            results.Clear();

            ContactFilter2D filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = enemyLayer
            };

            int count = hitboxCollider.Overlap(filter, results);

            for (int i = 0; i < count; i++)
            {
                if (results[i].TryGetComponent<IDamageable>(out var dmg))
                {
                    dmg.TakeDamage(damage);
                    hasHit = true;
                    return;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (hasHit) return;

            if (collision.TryGetComponent<IDamageable>(out var dmg))
            {
                hasHit = true;
                dmg.TakeDamage(damage);
            }
        }
    }
}
