using UnityEngine;

namespace BTF.Input
{
    public sealed class InputService
    {
        private Vector2 moveInput;
        private bool interactPressed;
        private bool attackPressed;
        private bool resetPressed;

        public void SetResetInput() => resetPressed = true;
        public void SetMoveInput(Vector2 input) => moveInput = input;

        public Vector2 GetMoveInput() => moveInput;

        public void SetInteractInput() => interactPressed = true;

        public void SetAttackInput() => attackPressed = true;

        public bool ConsumeAttackPress()
        {
            if(!attackPressed) return false;

            attackPressed = false;
            return true;
        }

        public bool ConsumeInteractPress()
        {
            if (!interactPressed) return false;

            interactPressed = false;
            return true;
        }

        public bool ConsumeReset()
        {
            if (!resetPressed) return false;
            resetPressed = false;
            return true;
        }
    }
}
