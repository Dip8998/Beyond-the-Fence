using BTF.Inventory;
using BTF.Player;
using System;

namespace BTF.Resource
{
    public sealed class BerryController
    {
        private readonly BerryModel model;
        private readonly InventoryController inventoryController;
        private readonly PlayerController player;

        private const int HEAL_AMOUNT = 10;

        public event Action OnBerryCollect;
        public event Action OnBerryRegrow;

        public BerryController(
            BerryModel model,
            InventoryController inventoryController,
            PlayerController player)
        {
            this.model = model;
            this.inventoryController = inventoryController;
            this.player = player;
        }

        public bool CanCollect() => model.IsAvailable;

        public void Collect()
        {
            if (!model.IsAvailable) return;

            model.Consume();
            inventoryController.AddBerry(1);

            player.Heal(HEAL_AMOUNT); 

            OnBerryCollect?.Invoke();
        }

        public void Regrow()
        {
            model.Regrow();
            OnBerryRegrow?.Invoke();
        }
    }
}
