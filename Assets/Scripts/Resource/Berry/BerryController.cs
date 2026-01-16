using BTF.Inventory;
using System;

namespace BTF.Resource
{
    public sealed class BerryController
    {
        private readonly BerryModel model;
        private readonly InventoryController inventory;

        public event Action OnBerryCollect;
        public event Action OnBerryRegrow;

        public BerryController(
            BerryModel model,
            InventoryController inventory)
        {
            this.model = model;
            this.inventory = inventory;
        }

        public bool CanCollect() => model.IsAvailable;

        public void Collect()
        {
            if (!model.IsAvailable) return;

            model.Consume();
            inventory.AddBerry(1);

            OnBerryCollect?.Invoke();
        }

        public void Regrow()
        {
            model.Regrow();
            OnBerryRegrow?.Invoke();
        }
    }
}
