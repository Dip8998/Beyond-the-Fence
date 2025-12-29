using BTF.Core.Services;
using BTF.Player.StateMachine;
using UnityEngine;

namespace BTF.Player
{
    public sealed class PlayerController
    {
        private readonly PlayerModel model;
        private readonly PlayerStateMachine stateMachine;
        private readonly InputService input;

        public PlayerController(PlayerModel model, InputService input)
        {
            this.model = model;
            this.input = input;

            stateMachine = new PlayerStateMachine(this, input);
        }

        public void Tick() => stateMachine.Update();

        public void SetVelocity(Vector2 velocity)
        {
            model.Velocity = velocity;
        }

        public void ChangeState(PlayerStates newState) => stateMachine?.ChangeStates(newState);

        public Vector2 GetVelocity() => model.Velocity;
        public float GetSpeed() => model.MoveSpeed;

        public bool HasMovementInput()
        {
            return input.MoveInput.sqrMagnitude > 0.01f;
        }

        public Vector2 GetMoveDirection()
        {
            return input.MoveInput.normalized;
        }

        public Vector2 GetRawMoveInput()
        {
            return input.MoveInput;
        }
    }
}
