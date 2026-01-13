using BTF.Inventory;

namespace BTF.Resource
{
    public sealed class GearController
    {
        private readonly InventoryController inventory;
        private readonly int amount;
        private bool collected;

        public GearController(InventoryController inventory, int amount)
        {
            this.inventory = inventory;
            this.amount = amount;
        }

        public bool CanCollect() => !collected;

        public void Collect()
        {
            if (collected) return;

            collected = true;
            inventory.AddGear(amount);
        }
    }
}
