using UnityEngine;

namespace BTF.Enemy
{
    public sealed class BigSlimeBossView : EnemyView
    {
        [SerializeField] private GameObject gearObject;

        private BossAttackHitbox attackHitbox;

        private static readonly int AttackTrigger =
            Animator.StringToHash("Attack");

        protected override void Awake()
        {
            base.Awake();

            attackHitbox = GetComponentInChildren<BossAttackHitbox>(true);

            if (attackHitbox == null)
            {
                Debug.LogError("[BigSlimeBossView] BossAttackHitbox NOT FOUND in children", this);
                return;
            }

            attackHitbox.Disable();
        }

        public void TriggerAttack()
        {
            animator.ResetTrigger(AttackTrigger);
            animator.SetTrigger(AttackTrigger);
        }

        public void Boss_EnableHitbox()
        {
            attackHitbox.Enable();
            attackHitbox.ApplyDamage();
        }

        public void Boss_DisableHitbox()
        {
            attackHitbox.Disable();
        }

        public override void OnDeath()
        {
            if (attackHitbox != null)
                attackHitbox.Disable();

            if (gearObject != null)
                gearObject.SetActive(true);

            base.OnDeath();
        }
    }
}
