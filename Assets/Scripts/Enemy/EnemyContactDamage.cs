using BTF.Player;
using UnityEngine;

namespace BTF.Enemy
{
    public class EnemyContactDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float damageInterval = 1f;

        private float damageTimer;
        private PlayerController player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out var view))
            {
                player = view.Controller;
                damageTimer = damageInterval;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out _))
            {
                player = null;
            }
        }

        private void Update()
        {
            if (player == null) return;
            if (player.IsInvincible) return;

            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                damageTimer = 0f;
                player.TakeDamage(damage);
            }
        }
    }
}
