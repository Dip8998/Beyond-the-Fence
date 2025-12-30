using UnityEngine;

namespace BTF.Input
{
    public sealed class InputService
    {
        private Vector2 moveInput;

        public void SetMoveInput(Vector2 input)
        {
            moveInput = input;
        }

        public Vector2 GetMoveInput()
        {
            return moveInput;
        }
    }
}
