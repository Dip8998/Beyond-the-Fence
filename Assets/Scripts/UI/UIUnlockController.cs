using UnityEngine;
using UnityEngine.UI;
using BTF.Inventory;
using BTF.Resource;
using BTF.Discovery;
using BTF.UI.Quest;
using System.Collections;

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
        [SerializeField] private float glowTimeout = 20f;

        private Coroutine inventoryGlowRoutine;
        private Coroutine questGlowRoutine;
        private InventoryController inventory;
        private BerryController berryController;
        private DiscoveryController discovery;

        private bool berryUnlocked;
        private bool inventoryUnlocked;
        private bool questUnlocked;
        private bool hasLoadedFromSave;

        public bool IsBerryUnlocked => berryUnlocked;
        public bool IsInventoryUnlocked => inventoryUnlocked;
        public bool IsQuestUnlocked => questUnlocked;

        public void SetUnlockedState(
            bool berry,
            bool inventory,
            bool quest)
        {
            hasLoadedFromSave = true;

            berryUnlocked = berry;
            inventoryUnlocked = inventory;
            questUnlocked = quest;

            RefreshUIState();
        }

        public void RefreshUIState()
        {
            berryButton.gameObject.SetActive(
            berryUnlocked
            );


            inventoryButton.gameObject.SetActive(
            inventoryUnlocked
            );


            questButton.gameObject.SetActive(
            questUnlocked
            );


            inventoryGlow.StopGlow();
            questGlow.StopGlow();
        }

        public void Bind(
            InventoryController inventory,
            BerryController berryController,
            DiscoveryController discovery)
        {
            this.inventory = inventory;
            this.discovery = discovery;

            if (!hasLoadedFromSave)
            {
                berryButton.gameObject.SetActive(false);
                inventoryButton.gameObject.SetActive(false);
                questButton.gameObject.SetActive(false);
            }

            inventory.OnInventoryChanged += OnInventoryChanged;

            if (!hasLoadedFromSave)
            {
                Invoke(nameof(UnlockQuest), questDelay);
            }
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

                inventoryGlowRoutine =
                StartCoroutine(StopGlowAfterTime(
                inventoryGlow,
                () => inventoryGlowRoutine = null
                ));
            }
        }

        private void UnlockQuest()
        {
            if (questUnlocked) return;

            questUnlocked = true;
            questButton.gameObject.SetActive(true);

            questGlow.StartGlow();
            discovery.Notify("Check your quest list");

            questGlowRoutine =
                StartCoroutine(StopGlowAfterTime(
                    questGlow,
                    () => questGlowRoutine = null
                ));
        }

        public void OnInventoryOpened()
        {
            inventoryGlow.StopGlow();
        }

        public void OnQuestOpened()
        {
            questGlow.StopGlow();
        }

        private IEnumerator StopGlowAfterTime(
            QuestButtonGlow glow,
            System.Action onFinished)
        {
            yield return new WaitForSeconds(glowTimeout);

            glow.StopGlow();
            onFinished?.Invoke();
        }

        private void OnDestroy()
        {
            if (inventory != null)
                inventory.OnInventoryChanged -= OnInventoryChanged;
        }

        public void ResetForNewGame()
        {
            hasLoadedFromSave = false;

            berryUnlocked = false;
            inventoryUnlocked = false;
            questUnlocked = false;

            berryButton.gameObject.SetActive(false);
            inventoryButton.gameObject.SetActive(false);
            questButton.gameObject.SetActive(false);

            CancelInvoke();
            Invoke(nameof(UnlockQuest), questDelay);
        }
    }
}