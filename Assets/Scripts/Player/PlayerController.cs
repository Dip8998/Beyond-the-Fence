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

        private const float INVINCIBLE_DURATION = .25f;

        public PlayerView View => view;
        public bool IsAttacking { get;  set; }
        public bool IsInvincible => model.IsInvincible;
        public bool IsLocked { get; private set; }

        public PlayerController(PlayerModel model, InputService inputService)
        {
            this.model = model;
            this.inputService = inputService;
            stateMachine = new PlayerStateMachine(this);
        }

        public void Bind(PlayerView view) => this.view = view;

        public void Tick()
        {
            if (IsLocked) return;

            UpdateInvincibility();
            stateMachine.Update();
        }

        public void ChangeState(PlayerStates newState) => stateMachine?.ChangeState(newState);

        public void Move(Vector2 dir)
        {
            if (IsLocked)
            {
                model.Velocity = Vector2.zero;
                return;
            }

            if (dir.sqrMagnitude < 0.001f)
            {
                model.Velocity = Vector2.zero;
                return;
            }

            model.Velocity = dir.normalized * model.MoveSpeed;
        }

        public bool ConsumeAttack()
        {
            if (!model.HasWeapon)
                return false;

            return inputService.ConsumeAttackPress();
        }

        public void ReceiveWeapon()
        {
            model.GiveWeapon();
        }

        public void StopMovement() => model.Velocity = Vector2.zero;

        private void UpdateInvincibility()
        {
            if (!model.IsInvincible) return;

            model.InvincibleTimer -= Time.deltaTime;
            if (model.InvincibleTimer <= 0f)
            {
                model.IsInvincible = false;
                view.StopFlash();
            }
        }

        public void TakeDamage(int damage)
        {
            if (model.IsInvincible) return;

            model.CurrentHP -= damage;

            StartInvincibility();

            if (model.CurrentHP <= 0)
                Die();
        }

        private void StartInvincibility()
        {
            model.IsInvincible = true;
            model.InvincibleTimer = INVINCIBLE_DURATION;
            view.StartFlash();
        }

        public bool HasFenceKey() => model.HasFenceKey;

        public void ReceiveFenceKey()
        {
            model.GiveFenceKey();
        }

        private void Die()
        {
            Debug.Log("PLAYER DIED");
            view.gameObject.SetActive(false);
        }

        public void Lock()
        {
            IsLocked = true;
            StopMovement();
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public void Heal(int amount) { }

        public Vector2 GetVelocity() => model.Velocity;

        public Vector2 GetMoveInput() => inputService.GetMoveInput();

        public bool HasMoveInput() => inputService.GetMoveInput().sqrMagnitude > 0.001f;
    }
}
