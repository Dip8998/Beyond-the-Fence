using UnityEngine;

namespace BTF.Inventory
{
    public class InventoryModel
    {
        public int WoodCount { get; private set; }
        public int BerryCount { get; private set; }
        public int GearCount { get; private set; }

        public InventoryModel(int wood = 0, int berry = 0, int gear = 0)
        {
            WoodCount = wood;
            BerryCount = berry;
            GearCount = gear;
        }

        public void AddWood(int c) => WoodCount += c;
        public void AddBerry(int c)
        {
            BerryCount = Mathf.Max(0, BerryCount + c);
        }
        public void AddGear(int c) => GearCount += c;

        public bool HasWood(int c) => WoodCount >= c;
        public bool HasGear(int c) => GearCount >= c;

        public void ConsumeWood(int c) => WoodCount -= c;
        public void ConsumeGear(int c) => GearCount -= c;
    }
}
