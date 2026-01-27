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
            dialogue = new VillagerDialogue(controller);
        }

        public void Interact()
        {
            if (context.DialogueController.IsPlaying)
                return;

            controller.TryActivate();

            var task = context.Quest.CurrentQuest?.GetCurrentTask();

            if (task != null && task.Text != "Talk to the villager")
            {
                controller.TryProgress(bossIslandPoint);
            }

            context.DialogueController.StartDialogue(
                context.DialogueRunner.Run(dialogue),
                OnDialogueFinished
            );
        }

        private void OnDialogueFinished()
        {
            var task = context.Quest.CurrentQuest?.GetCurrentTask();

            if (task != null && task.Text == "Talk to the villager")
            {
                controller.OnFirstConversationFinished();
            }
        }
    }
}