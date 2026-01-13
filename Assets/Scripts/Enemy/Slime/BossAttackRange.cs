using UnityEngine;
using BTF.Player;

namespace BTF.Enemy
{
    public sealed class BossAttackRange : MonoBehaviour
    {
        [SerializeField] private float attackCooldown = 2f;

        private PlayerController player;
        private BigSlimeBossView bossView;
        private float timer;
        private bool playerInRange;

        private void Awake()
        {
            bossView = GetComponentInParent<BigSlimeBossView>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out var view))
            {
                player = view.Controller;
                playerInRange = true;
                timer = attackCooldown; 
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerView>())
            {
                player = null;
                playerInRange = false;
            }
        }

        private void Update()
        {
            if (!playerInRange || bossView == null)
                return;

            timer += Time.deltaTime;

            if (timer >= attackCooldown)
            {
                timer = 0f;
                bossView.TriggerAttack();
            }
        }
    }
}
