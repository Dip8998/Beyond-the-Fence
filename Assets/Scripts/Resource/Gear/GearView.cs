using BTF.Game;
using BTF.Interfaces;
using BTF.Inventory;
using UnityEngine;

namespace BTF.Resource
{
    public sealed class GearView : MonoBehaviour, IInteractable
    {
        [SerializeField] private int gearAmount = 1;

        private InventoryController inventory;
        private GameContext gameContext;

        public void Bind(InventoryController inventory, GameContext gameContext)
        {
            this.inventory = inventory;
            this.gameContext = gameContext;
        }

        public void Interact()
        {
            if (inventory == null)
            {
                Debug.LogError("GearView not bound!");
                return;
            }

            inventory.AddGear(gearAmount);
            gameContext.Quest.CompleteTask(3);
            gameObject.SetActive(false);
        }
    }
}
