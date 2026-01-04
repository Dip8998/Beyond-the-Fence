using BTF.Inventory;
using System;

namespace BTF.Resource
{
    public sealed class TreeController
    {
        private readonly TreeModel model;
        private readonly InventoryController inventoryController;

        public event Action OnTreeCut;
        public event Action OnTreeRegrow;

        public TreeController(TreeModel model, InventoryController inventoryController)
        {
            this.model = model;
            this.inventoryController = inventoryController;
        }

        public void TakeDamage(int damage)
        {
            model?.ReduceHP(damage);

            if(model.CurrentHP <= 0)
            {
                inventoryController?.AddWood(1);
                OnTreeCut?.Invoke();
            }
        }

        public void Regrow()
        {
            model?.Reset();
            OnTreeRegrow?.Invoke();
        }
    }
}
