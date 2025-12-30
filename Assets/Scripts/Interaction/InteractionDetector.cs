using BTF.Interfaces;
using UnityEngine;

namespace BTF.Interaction
{
    public sealed class InteractionDetector : MonoBehaviour
    {
        private InteractionSystem interactionSystem;

        public void Bind(InteractionSystem interactionSystem) => this.interactionSystem = interactionSystem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactionSystem.AddInteractables(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactionSystem.RemoveInteractables(interactable);    
            }
        }
    }
}
