using BTF.Input;
using BTF.Player.PSM;
using UnityEngine;

namespace BTF.Player
{
    public sealed class PlayerController
    {
        private readonly PlayerModel model;
        private PlayerView view;
        private readonly PlayerStateMachine stateMachine;
        private InputService inputService;
        
        public PlayerView View => view;

        public PlayerController(PlayerModel model, InputService inputService)
        {
            this.model = model;
            this.inputService = inputService;
            stateMachine = new PlayerStateMachine(this);
        }

        public void Tick()
        {
            stateMachine.Update();
        }

        public void Bind(PlayerView view) => this.view = view;

        public void ChangeState(PlayerStates newState) => stateMachine?.ChangeState(newState);

        public void Move(Vector2 dir)
        {
            if(dir.sqrMagnitude < 0.001f)
            {
                model.Velocity = Vector2.zero;
                return;
            }

            model.Velocity = dir.normalized * model.MoveSpeed;
        }

        public bool ConsumeAttack()
        {
            return inputService.ConsumeAttackPress();
        }

        public void StopMovement() => model.Velocity = Vector2.zero;

        public void TakeDamage(int damage) { } 

        public void Heal(int amount) { }

        public Vector2 GetVelocity() => model.Velocity;

        public Vector2 GetMoveInput() => inputService.GetMoveInput();

        public bool HasMoveInput() => inputService.GetMoveInput().sqrMagnitude > 0.001f;
    }
}
