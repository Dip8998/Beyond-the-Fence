using BTF.Dialogue;
using BTF.Game;
using BTF.Interfaces;
using UnityEngine;

namespace BTF.Villager
{
    public sealed class VillagerBoatQuestView : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform bossIslandPoint;

        private VillagerBoatQuestController controller;
        private GameContext context;
        private VillagerDialogue dialogue;

        public void Bind(
            VillagerBoatQuestController controller,
            GameContext context)
        {
            this.controller = controller;
            this.context = context;

            dialogue = new VillagerDialogue();
        }

        public void Interact()
        {
            if (context.DialogueController.IsPlaying)
                return;

            controller.TryActivate();

            if (controller.GetState() == VillagerBoatQuestState.Inactive)
                return;

            context.DialogueController.StartDialogue(
                context.DialogueRunner.Run(dialogue),
                () =>
                {
                    context.Quest.CompleteTask(0);
                    controller.Interact(bossIslandPoint);
                }
            );
        }
    }
}
