using BTF.Interfaces;
using UnityEngine;

namespace BTF.Player.PSM.States
{
    public class PlayerIdleState : IState<PlayerController>
    {
        private PlayerController owner;

        public void OnStateEnter()
        {
            owner.Move(Vector2.zero);   
        }

        public void SetOwner(PlayerController owner)
        {
            this.owner = owner;
        }

        public void Update()
        {
            if (owner.HasMoveInput())
            {
                owner.ChangeState(PlayerStates.Move);
            }
        }

        public void OnStateExit() { }
    }
}
