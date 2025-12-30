using UnityEngine;

namespace BTF.Input
{
    public sealed class InputService
    {
        private Vector2 moveInput;
        private bool interactPressed;

        public void SetMoveInput(Vector2 input) => moveInput = input;

        public Vector2 GetMoveInput() => moveInput;

        public void SetInteractInput() => interactPressed = true;

        public bool ConsumeInput()
        {
            if (!interactPressed) return false;

            interactPressed = false;
            return true;
        }
    }
}
