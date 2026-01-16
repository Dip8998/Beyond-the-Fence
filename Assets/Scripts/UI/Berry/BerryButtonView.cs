using BTF.Inventory;
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

        public void Bind(
            BerryButtonController controller,
            InventoryController inventory)
        {
            this.controller = controller;
            this.inventory = inventory;

            button.onClick.AddListener(OnClick);
            inventory.OnInventoryChanged += Refresh;

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

            if (count <= 0)
            {
                button.interactable = false;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                button.interactable = true;
            }
        }

    }
}
