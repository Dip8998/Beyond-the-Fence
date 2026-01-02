using BTF.Player.PSM.States;
using BTF.StateMachine;

namespace BTF.Player.PSM
{
    public class PlayerStateMachine : GenericStateMachine<PlayerController>
    {
        public PlayerStateMachine(PlayerController owner) : base(owner)
        {
            States.Add(PlayerStates.Idle, new PlayerIdleState());
            States.Add(PlayerStates.Move, new PlayerMoveState());
            States.Add(PlayerStates.Attack, new PlayerAttackState());

            SetOwner();

            ChangeState(PlayerStates.Idle);
        }
    }
}
