using System;

namespace BTF.Inventory
{
    public class InventoryController
    {
        private readonly InventoryModel model;

        public event Action OnInventoryChanged;

        public InventoryController(InventoryModel model)
        {
            this.model = model;
        }

        public int GetBerryCount() => model.BerryCount;

        public bool HasBerry(int count = 1)
        {
            return model.BerryCount >= count;
        }

        public bool ConsumeBerry(int count = 1)
        {
            if (!HasBerry(count)) return false;

            model.AddBerry(-count); 
            Notify();
            Log();
            return true;
        }

        public void AddWood(int count)
        {
            if (count <= 0) return;
            model.AddWood(count);
            Notify();
            Log();
        }

        public void AddBerry(int count)
        {
            if (count <= 0) return;
            model.AddBerry(count);
            Notify();               
            Log();
        }

        public bool HasWood(int count) => model.HasWood(count);

        public bool ConsumeWood(int count)
        {
            if (!HasWood(count)) return false;
            model.ConsumeWood(count);
            Notify();
            Log();
            return true;
        }

        public void AddGear(int count)
        {
            model.AddGear(count);
            Notify();
            Log();
        }

        public bool HasGear(int count) => model.HasGear(count);

        public bool ConsumeGear(int count)
        {
            if (!model.HasGear(count)) return false;
            model.ConsumeGear(count);
            Notify();
            Log();
            return true;
        }

        private void Notify() => OnInventoryChanged?.Invoke();

        private void Log()
        {
            UnityEngine.Debug.Log(
                $"Inventory → Wood: {model.WoodCount}, Berry: {model.BerryCount}, Gear: {model.GearCount}"
            );
        }

        public int GetWoodCount() => model.WoodCount;
        public int GetGearCount() => model.GearCount;
    }
}
