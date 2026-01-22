using BTF.Interfaces;
using System.Collections.Generic;

namespace BTF.Interaction
{
    public sealed class InteractionSystem
    {
        private readonly List<IInteractable> interactables = new();
        private readonly BTF.Game.GameContext context;

        public InteractionSystem(BTF.Game.GameContext context)
        {
            this.context = context;
        }

        public void AddInteractables(IInteractable interactable)
        {
            if (interactables.Contains(interactable))
                return;

            interactables.Add(interactable);
        }

        public void RemoveInteractables(IInteractable interactable)
        {
            interactables.Remove(interactable);
        }

        public void TryInteract()
        {
            if (context.Player.IsMovementLocked)
                return;

            if (interactables.Count == 0)
                return;

            interactables[0].Interact();
        }
    }
}