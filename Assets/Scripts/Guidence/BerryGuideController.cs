using UnityEngine;
using BTF.Tutorial;
using BTF.Dialogue;

namespace BTF.Guidance
{
    public sealed class BerryGuideController : MonoBehaviour
    {
        [SerializeField] private WorldGuideArrow arrow;
        [SerializeField] private Transform berryTarget;

        private DialogueController dialogueController;
        private DialogueRunner dialogueRunner;

        private bool completed;

        public void Bind(
            DialogueController dialogueController,
            DialogueRunner dialogueRunner)
        {
            this.dialogueController = dialogueController;
            this.dialogueRunner = dialogueRunner;

            if (TutorialFlags.FoodTutorialShown)
            {
                arrow.Hide();
                completed = true;
                return;
            }

            arrow.gameObject.SetActive(true);
            arrow.SetTarget(berryTarget);
        }

        public void OnBerryCollected()
        {
            if (completed) return;

            completed = true;
            TutorialFlags.FoodTutorialShown = true;

            arrow.Hide();

            dialogueController.StartDialogue(
                dialogueRunner.Run(new FoodTutorialDialogue())
            );
        }
    }
}