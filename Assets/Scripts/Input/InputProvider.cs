using UnityEngine;
using UnityEngine.InputSystem;

namespace BTF.Input
{
    public sealed class InputProvider : MonoBehaviour
    {
        private PlayerInputActions actions;
        private InputService inputService;

        public void Bind(InputService inputService) => this.inputService = inputService;

        private void Awake()
        {
            actions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            actions.Player.Move.performed += OnMovePerformed;
            actions.Player.Move.canceled += OnMoveCanceled;
            actions.Player.Interact.performed += OnInteractPerformed;
            actions.Player.Attack.performed += OnAttackPerformed;
            actions.Player.Reset.performed += _ => inputService.SetResetInput();
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Player.Move.performed -= OnMovePerformed;
            actions.Player.Move.canceled -= OnMoveCanceled;
            actions.Player.Interact.performed -= OnInteractPerformed;
            actions.Player.Attack.performed -= OnAttackPerformed;
            actions.Player.Reset.performed -= _ => inputService.SetResetInput();
            actions.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            inputService.SetMoveInput(ctx.ReadValue<Vector2>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            inputService.SetMoveInput(Vector2.zero);
        }

        private void OnInteractPerformed(InputAction.CallbackContext ctx)
        {
            inputService.SetInteractInput();
        }

        private void OnAttackPerformed(InputAction.CallbackContext ctx)
        {
            inputService.SetAttackInput();
        }
    }
}