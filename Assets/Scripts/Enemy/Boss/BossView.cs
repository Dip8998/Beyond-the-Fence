using UnityEngine;

namespace BTF.Boss
{
    public sealed class BossView : BossViewBase
    {
        private static readonly int AttackTrigger =
            Animator.StringToHash("AttackBoss");

        public override void TriggerAttack()
        {
            animator.ResetTrigger(AttackTrigger);
            animator.SetTrigger(AttackTrigger);
        }

        public override void OnDeath()
        {
            Debug.Log("FINAL BOSS DEFEATED");
            base.OnDeath();
        }
    }
}
