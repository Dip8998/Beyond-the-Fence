using BTF.Inventory;
using BTF.Tutorial;
using BTF.Dialogue;
using UnityEngine;

namespace BTF.Guidance
{
    public sealed class BerryGuideController : MonoBehaviour
    {
        [SerializeField] private WorldGuideArrow arrow;
        [SerializeField] private Transform berryTarget;

        private DialogueController dialogueController;
        private DialogueRunner dialogueRunner;
        private InventoryController inventory;

        private bool completed;

        public void Bind(
            DialogueController dialogueController,
            DialogueRunner dialogueRunner,
            InventoryController inventory)
        {
            this.dialogueController = dialogueController;
            this.dialogueRunner = dialogueRunner;
            this.inventory = inventory;

            if (TutorialFlags.FoodTutorialShown || inventory.HasBerry(1))
            {
                Complete();
                return;
            }

            arrow.gameObject.SetActive(true);
            arrow.SetTarget(berryTarget);

            inventory.OnBerryAdded += OnBerryAdded;
        }

        private void OnBerryAdded(int totalBerries)
        {
            if (completed) return;

            Complete();

            dialogueController.StartDialogue(
                dialogueRunner.Run(new FoodTutorialDialogue())
            );
        }

        private void Complete()
        {
            completed = true;
            TutorialFlags.FoodTutorialShown = true;

            arrow.Hide();

            if (inventory != null)
                inventory.OnBerryAdded -= OnBerryAdded;
        }

        private void OnDestroy()
        {
            if (inventory != null)
                inventory.OnBerryAdded -= OnBerryAdded;
        }
    }
}