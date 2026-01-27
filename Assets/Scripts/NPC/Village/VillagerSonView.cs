using BTF.Dialogue;
using BTF.Game;
using BTF.Interfaces;
using UnityEngine;

namespace BTF.Villager
{
    public sealed class VillagerSonView : MonoBehaviour, IInteractable
    {
        private GameContext context;
        private VillagerSonDialogue dialogue;

        public void Bind(GameContext context)
        {
            this.context = context;
            dialogue = new VillagerSonDialogue();
        }

        public void Interact()
        {
            if (context.DialogueController.IsPlaying)
                return;

            context.DialogueController.StartDialogue(
                context.DialogueRunner.Run(dialogue)
            );
        }
    }
}