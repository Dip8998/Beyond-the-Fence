using BTF.Inventory;
using UnityEngine;

namespace BTF.World
{
    public sealed class BridgeController
    {
        private readonly InventoryController inventory;
        private readonly GameObject bridgeObject;
        private readonly GameObject bridgeBlocker;
        private readonly int requiredWood;

        public bool IsBuilt { get; private set; }

        public BridgeController(
            InventoryController inventory,
            GameObject bridgeObject,
            GameObject bridgeBlocker,
            int requiredWood)
        {
            this.inventory = inventory;
            this.bridgeObject = bridgeObject;
            this.bridgeBlocker = bridgeBlocker;
            this.requiredWood = requiredWood;

            bridgeObject.SetActive(false);
            bridgeBlocker.SetActive(true);
        }

        public bool CanBuild()
        {
            return !IsBuilt && inventory.HasWood(requiredWood);
        }

        public void Build()
        {
            if (IsBuilt)
                return;

            inventory.ConsumeWood(requiredWood);

            bridgeObject.SetActive(true);
            bridgeBlocker.SetActive(false);

            IsBuilt = true;

            Debug.Log("Bridge built! Slime Area unlocked.");
        }
    }
}
