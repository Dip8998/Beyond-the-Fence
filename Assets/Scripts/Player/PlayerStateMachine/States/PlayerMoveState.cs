using BTF.Interfaces;

namespace BTF.Player.PSM.States
{
    public class PlayerMoveState : IState<PlayerController>
    {
        private PlayerController owner;

        public void SetOwner(PlayerController owner) => this.owner = owner;

        public void OnStateEnter() { }

        public void Update()
        {
            owner.Move(owner.GetMoveInput());

            if (!owner.HasMoveInput())
            {
                owner.ChangeState(PlayerStates.Idle);
            }
        }

        public void OnStateExit() { }   
    }
}
