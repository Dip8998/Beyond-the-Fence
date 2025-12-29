using UnityEngine;
using UnityEngine.InputSystem;

namespace BTF.Core.Services
{
    public sealed class InputService
    {
        public Vector2 MoveInput { get; private set; }
        private PlayerInputAction actions;

        public void Enable()
        {
            actions = new PlayerInputAction();
            actions.Gameplay.Move.performed += OnMove;
            actions.Gameplay.Move.canceled += OnMoveCanceled;

            actions.Enable();
        }

        public void Disable() => actions.Disable();

        private void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();

        private void OnMoveCanceled(InputAction.CallbackContext ctx) => MoveInput = Vector2.zero;

        public void SetMoveInput(Vector2 input)
        {
            MoveInput = input;
        }
    }
}
