using BTF.Enemy;
using UnityEngine;

namespace BTF.Boss
{
    public abstract class BossViewBase : EnemyView
    {
        protected BossAttackHitbox attackHitbox;

        protected override void Awake()
        {
            base.Awake();

            attackHitbox = GetComponentInChildren<BossAttackHitbox>(true);
            if (attackHitbox == null)
            {
                Debug.LogError("[BossViewBase] AttackHitbox missing", this);
                return;
            }

            attackHitbox.Disable();
        }

        public abstract void TriggerAttack();

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
            attackHitbox.Disable();
            base.OnDeath();
        }
    }
}
