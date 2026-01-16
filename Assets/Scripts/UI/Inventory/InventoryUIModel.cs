namespace BTF.UI.Inventory
{
    public sealed class InventoryUIModel
    {
        public int Wood { get; private set; }
        public int Gear { get; private set; }

        public bool IsEmpty => Wood == 0 && Gear == 0;

        public void Set(int wood, int gear)
        {
            Wood = wood;
            Gear = gear;
        }
    }
}
