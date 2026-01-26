using UnityEngine;
using UnityEngine.UI;
using BTF.UI;

namespace BTF.UI.Quest
{
    public sealed class ButtonPanelToggle : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button button;

        [Header("Unlock Controller")]
        [SerializeField] private UIUnlockController unlockController;
        [SerializeField] private bool isInventoryButton;
        [SerializeField] private bool isQuestButton;

        private bool isOpen;

        private void Awake()
        {
            button.onClick.AddListener(Toggle);
            panel.SetActive(false);
            isOpen = false;
        }

        private void Toggle()
        {
            isOpen = !isOpen;
            panel.SetActive(isOpen);

            if (!isOpen) return;

            if (isInventoryButton)
                unlockController.OnInventoryOpened();

            if (isQuestButton)
                unlockController.OnQuestOpened();
        }
    }
}