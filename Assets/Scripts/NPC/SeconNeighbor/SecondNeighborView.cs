using BTF.Dialogue;
using BTF.FirstNB;
using BTF.Game;
using BTF.Interfaces;
using UnityEngine;

namespace BTF.SeconNB
{
    public sealed class SecondNeighborView : MonoBehaviour, IInteractable
    {
        private SecondNeighborController controller;
        private FirstNeighborController firstNeighbor;
        private GameContext context;
        private SecondNeighborDialogue dialogue;

        public void Bind(
            SecondNeighborController controller,
            FirstNeighborController firstNeighbor,
            GameContext context)
        {
            this.controller = controller;
            this.firstNeighbor = firstNeighbor;
            this.context = context;

            dialogue = new SecondNeighborDialogue(firstNeighbor, controller);
        }

        public void Interact()
        {
            context.DialogueController.StartDialogue(
                context.DialogueRunner.Run(dialogue),
                OnDialogueFinished
            );
        }

        private void OnDialogueFinished()
        {
            bool asked =
                firstNeighbor.GetState() == FirstNeighborState.AskedForHelp;

            if (!asked)
                return;

            if (controller.GetState() == SecondNeighborState.Confessed)
                return;

            controller.OnPlayerInteract(asked);

            if (controller.GetState() == SecondNeighborState.Confessed)
            {
                firstNeighbor.ResolveConflict();
                context.Quest.Advance();
            }
        }
    }
}
