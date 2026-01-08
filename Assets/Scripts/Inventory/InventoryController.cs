namespace BTF.Inventory
{
    public class InventoryController
    {
        private readonly InventoryModel model;

        public InventoryController(InventoryModel model)
        {
            this.model = model;
        }

        public void AddWood(int count)
        {
            if (count <= 0) return;
            model.AddWood(count);
            Log();
        }

        public void AddBerry(int count)
        {
            if (count <= 0) return;
            model.AddBerry(count);
            Log();
        }

        public bool HasWood(int count)
        {
            return model.HasWood(count);
        }

        public bool ConsumeWood(int count)
        {
            if (!HasWood(count)) return false;
            model.ConsumeWood(count);
            Log();
            return true;
        }

        public void AddGear(int count)
        {
            model.AddGear(count);
            Log();
        }

        public bool HasGear(int count)
        {
            return model.HasGear(count);
        }

        public bool ConsumeGear(int count)
        {
            if (!model.HasGear(count)) return false;
            model.ConsumeGear(count);
            Log();
            return true;
        }

        public int GetWoodCount()
        {
            return model.WoodCount;
        }

        private void Log()
        {
            UnityEngine.Debug.Log(
                $"Inventory → Wood: {model.WoodCount}, Berry: {model.BerryCount}"
            );
        }
    }
}
