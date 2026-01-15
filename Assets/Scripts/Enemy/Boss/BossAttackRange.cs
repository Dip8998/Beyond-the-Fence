using BTF.Boss;
using BTF.Player;
using UnityEngine;

namespace BTF.Enemy
{
    public sealed class BossAttackRange : MonoBehaviour
    {
        [SerializeField] private float attackCooldown = 2f;

        private PlayerController player;
        private BossViewBase boss;
        private EnemyController bossController;

        private float timer;
        private bool playerInRange;

        private void Awake()
        {
            boss = GetComponentInParent<BossViewBase>();

            if (boss == null)
            {
                Debug.LogError("[BossAttackRange] BossViewBase NOT found", this);
                enabled = false;
            }
        }

        private void Start()
        {
            bossController = boss.Controller;

            if (bossController == null)
            {
                Debug.LogError(
                    "[BossAttackRange] EnemyController still missing in Start()",
                    this
                );
                enabled = false;
                return;
            }

            bossController.OnEnemyDied += OnBossDied;
        }

        private void OnDestroy()
        {
            if (bossController != null)
                bossController.OnEnemyDied -= OnBossDied;
        }

        private void OnBossDied()
        {
            if (player != null)
                player.UnlockMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out var view))
            {
                player = view.Controller;
                playerInRange = true;
                timer = attackCooldown;

                player.LockMovement(); 
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out _))
            {
                playerInRange = false;
                player = null;
            }
        }

        private void Update()
        {
            if (!playerInRange || boss == null)
                return;

            timer += Time.deltaTime;

            if (timer >= attackCooldown)
            {
                timer = 0f;
                boss.TriggerAttack();
            }
        }
    }
}
