using BTF.Inventory;
using BTF.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BTF.UI
{
    public sealed class BerryButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI countText;

        private BerryButtonController controller;
        private InventoryController inventory;
        private PlayerController player;

        public void Bind(
            BerryButtonController controller,
            InventoryController inventory,
            PlayerController player)
        {
            this.controller = controller;
            this.inventory = inventory;
            this.player = player;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);

            inventory.OnInventoryChanged -= Refresh;
            inventory.OnInventoryChanged += Refresh;

            player.OnHealthChanged -= Refresh;
            player.OnHealthChanged += Refresh;

            Refresh();
        }

        private void OnClick()
        {
            controller.UseBerry();
        }

        private void Refresh()
        {
            int count = inventory.GetBerryCount();
            countText.text = count.ToString();

            button.interactable = controller.CanUseBerry();
        }

        private void OnDestroy()
        {
            if (inventory != null)
                inventory.OnInventoryChanged -= Refresh;

            if (player != null)
                player.OnHealthChanged -= Refresh;
        }
    }
}
