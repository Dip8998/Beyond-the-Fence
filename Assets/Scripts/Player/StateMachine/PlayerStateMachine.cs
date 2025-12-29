using BTF.Core.Services;
using BTF.Core.StateMachine;
using BTF.Player.StateMachine.States;

namespace BTF.Player.StateMachine
{
    public sealed class PlayerStateMachine : GenericStateMachine<PlayerController>
    {
        public PlayerStateMachine(PlayerController owner, InputService input) : base(owner)
        {
            States.Add(PlayerStates.Idle, new PlayerIdleState());
            States.Add(PlayerStates.Move, new PlayerMoveState());

            SetOwnerToStates();
            ChangeStates(PlayerStates.Idle);
        }
    }
}
