using BTF.Core.StateMachine;
using UnityEngine;

namespace BTF.Player.StateMachine.States
{
    public sealed class PlayerMoveState : IState<PlayerController>
    {
        private PlayerController owner;

        public void SetOwner(PlayerController owner) => this.owner = owner;

        public void OnStateEnter() { }

        public void Update()
        {
            if (!owner.HasMovementInput())
            {
                owner.ChangeState(PlayerStates.Idle);
                return;
            }

            Vector2 velocity = owner.GetMoveDirection() * owner.GetSpeed();
            owner.SetVelocity(velocity);
        }

        public void OnStateExit()
        {
            owner.SetVelocity(Vector2.zero);
        }
    }
}
