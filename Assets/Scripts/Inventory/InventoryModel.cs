namespace BTF.Inventory
{
	public class InventoryModel
	{
		public int WoodCount { get; private set; }
		public int BerryCount { get; private set; }

		public InventoryModel(int woodCount = 0, int berryCount = 0)
		{
			WoodCount = woodCount;
			BerryCount = berryCount;
		}

        public void AddWood(int count)
        {
            WoodCount += count;
        }

        public void AddBerry(int count)
        {
            BerryCount += count;
        }

        public bool HasWood(int count) => WoodCount >= count;

        public void ConsumeWood(int count)
        {
            WoodCount -= count;
        }
    }
}

