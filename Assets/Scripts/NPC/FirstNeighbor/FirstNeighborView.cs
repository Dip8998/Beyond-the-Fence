using BTF.Dialogue;
using BTF.Discovery;
using BTF.Game;
using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.FirstNB
{
    public sealed class FirstNeighborView : MonoBehaviour, IInteractable
    {
        private FirstNeighborController controller;
        private PlayerController player;
        private GameContext context;
        private FirstNeighborDialogue dialogue;

        public void Bind(
            FirstNeighborController controller,
            PlayerController player,
            GameContext context)
        {
            this.controller = controller;
            this.player = player;
            this.context = context;

            dialogue = new FirstNeighborDialogue(controller);
        }

        public void Interact()
        {
            context.DialogueController.StartDialogue(
               context.DialogueRunner.Run(dialogue),
               () =>
               {
                   if (controller.GetState() == FirstNeighborState.Idle)
                   {
                       controller.OnPlayerInteract();
                   }

                   if (controller.GetState() == FirstNeighborState.ConflictResolved)
                   {
                       controller.GiveKey(player);
                       context.Discovery.TryDiscover(DiscoverableItem.FenceKey);
                   }
               });
        }
    }
}
