using BTF.Core.StateMachine;
using UnityEngine;

namespace BTF.Player.StateMachine.States
{
	public sealed class PlayerIdleState : IState<PlayerController>
	{
		private PlayerController owner;

		public void SetOwner(PlayerController owner) => this.owner = owner;

		public void OnStateEnter()
		{
			owner.SetVelocity(Vector2.zero);
		}

		public void Update()
		{
			if (owner.HasMovementInput())
			{
				owner.ChangeState(PlayerStates.Move);
			}
		}

		public void OnStateExit() { }
	}
}
