using BTF.Inventory;

namespace BTF.UI.Inventory
{
    public sealed class InventoryUIController
    {
        private readonly InventoryUIModel model;
        private readonly InventoryUIView view;
        private readonly InventoryController inventory;

        public InventoryUIController(
            InventoryUIModel model,
            InventoryUIView view,
            InventoryController inventory)
        {
            this.model = model;
            this.view = view;
            this.inventory = inventory;

            Refresh();
            inventory.OnInventoryChanged += Refresh;
        }

        private void Refresh()
        {
            model.Set(
                inventory.GetWoodCount(),
                inventory.GetGearCount()
            );

            if (model.IsEmpty)
                view.ShowEmpty();
            else
                view.ShowItems(model.Wood, model.Gear);
        }

        public void Dispose()
        {
            inventory.OnInventoryChanged -= Refresh;
        }
    }
}
