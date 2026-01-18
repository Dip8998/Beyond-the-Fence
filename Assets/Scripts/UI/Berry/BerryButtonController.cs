using BTF.Inventory;
using BTF.Player;

namespace BTF.UI
{
    public sealed class BerryButtonController
    {
        private readonly InventoryController inventory;
        private readonly PlayerController player;

        private const int HEAL_AMOUNT = 10;

        public BerryButtonController(
            InventoryController inventory,
            PlayerController player)
        {
            this.inventory = inventory;
            this.player = player;
        }

        public void UseBerry()
        {
            if (player.GetCurrentHP() >= player.GetMaxHP())
                return;

            if (!inventory.ConsumeBerry(1))
                return;

            player.Heal(HEAL_AMOUNT);
        }

        public bool CanUseBerry()
        {
            return inventory.HasBerry(1) &&
                   player.GetCurrentHP() < player.GetMaxHP();
        }
    }
}
