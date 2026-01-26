using UnityEngine;
using UnityEngine.UI;
using BTF.Inventory;
using BTF.Resource;
using BTF.Discovery;
using BTF.UI.Quest;

namespace BTF.UI
{
    public sealed class UIUnlockController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button berryButton;
        [SerializeField] private Button inventoryButton;
        [SerializeField] private Button questButton;

        [Header("Glow")]
        [SerializeField] private QuestButtonGlow inventoryGlow;
        [SerializeField] private QuestButtonGlow questGlow;

        [Header("Quest Delay")]
        [SerializeField] private float questDelay = 10f;

        private InventoryController inventory;
        private BerryController berryController;
        private DiscoveryController discovery;

        private bool berryUnlocked;
        private bool inventoryUnlocked;
        private bool questUnlocked;

        public void Bind(
            InventoryController inventory,
            BerryController berryController, 
            DiscoveryController discovery)
        {
            this.inventory = inventory;
            this.discovery = discovery;

            berryButton.gameObject.SetActive(false);
            inventoryButton.gameObject.SetActive(false);
            questButton.gameObject.SetActive(false);

            inventory.OnInventoryChanged += OnInventoryChanged;

            Invoke(nameof(UnlockQuest), questDelay);
        }

        private void OnInventoryChanged()
        {
            if (!berryUnlocked && inventory.GetBerryCount() > 0)
            {
                berryUnlocked = true;
                berryButton.gameObject.SetActive(true);
            }

            if (!inventoryUnlocked &&
                (inventory.GetWoodCount() > 0 ||
                 inventory.GetGearCount() > 0))
            {
                inventoryUnlocked = true;
                inventoryButton.gameObject.SetActive(true);

                inventoryGlow.StartGlow();
                discovery.Notify("Check inventory");
            }
        }

        private void UnlockQuest()
        {
            if (questUnlocked) return;

            questUnlocked = true;
            questButton.gameObject.SetActive(true);

            questGlow.StartGlow();
            discovery.Notify("Check your quest list");
        }

        public void OnInventoryOpened()
        {
            inventoryGlow.StopGlow();
        }

        public void OnQuestOpened()
        {
            questGlow.StopGlow();
        }

        private void OnDestroy()
        {
            if (inventory != null)
                inventory.OnInventoryChanged -= OnInventoryChanged;
        }
    }
}