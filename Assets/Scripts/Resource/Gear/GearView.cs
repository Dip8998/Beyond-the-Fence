using BTF.Interfaces;
using BTF.Inventory;
using UnityEngine;

namespace BTF.Resource
{
    public sealed class GearView : MonoBehaviour, IInteractable
    {
        [SerializeField] private int gearAmount = 1;

        private InventoryController inventory;

        public void Bind(InventoryController inventory)
        {
            this.inventory = inventory;
        }

        public void Interact()
        {
            if (inventory == null)
            {
                Debug.LogError("GearView not bound!");
                return;
            }

            inventory.AddGear(gearAmount);
            gameObject.SetActive(false);
        }
    }
}
