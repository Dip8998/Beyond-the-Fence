using BTF.Interfaces;

namespace BTF.Player.PSM.States
{
    public class PlayerAttackState : IState<PlayerController>
    {
        private PlayerController owner;

        public void SetOwner(PlayerController owner) => this.owner = owner;
        
        public void OnStateEnter()
        {
            owner.StopMovement();
            owner.View.PlayAttackAnimation();
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}
