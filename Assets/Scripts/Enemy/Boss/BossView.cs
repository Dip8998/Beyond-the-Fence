using UnityEngine;

namespace BTF.Boss
{
    public sealed class BossView : BossViewBase
    {
        [SerializeField] private GameObject villagerSon;

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
            gameContext.Quest.Advance();
            if (villagerSon != null)
                villagerSon.SetActive(true);
            base.OnDeath();
        }
    }
}
