using BTF.Game;
using BTF.Quest;
using System;
using System.Threading;

namespace BTF.Inventory
{
    public class InventoryController
    {
        private readonly InventoryModel model;
        private QuestController questController;
        public event Action OnInventoryChanged;
        public event Action<int> OnBerryAdded;
        public bool IsLoading { get; set; }
        public InventoryController(InventoryModel model, QuestController questController)
        {
            this.model = model;
            this.questController = questController;
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

            if (model.WoodCount >= 4)
            {
                questController.CompleteTask(1);
            }
        }

        public void AddBerry(int count)
        {
            if (count <= 0) return;
            model.AddBerry(count);
            if (!IsLoading)
                OnBerryAdded?.Invoke(count);
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

        public void ResetInventory()
        {
            model.Clear(); 
            Notify();
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
