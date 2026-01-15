using BTF.Boss;
using UnityEngine;

namespace BTF.Enemy
{
    public sealed class BigSlimeBossView : BossViewBase
    {
        [SerializeField] private GameObject gearObject;

        private static readonly int AttackTrigger =
            Animator.StringToHash("Attack");

        public override void TriggerAttack()
        {
            animator.ResetTrigger(AttackTrigger);
            animator.SetTrigger(AttackTrigger);
        }

        public override void OnDeath()
        {
            if (gearObject != null)
                gearObject.SetActive(true);

            base.OnDeath();
        }
    }
}
