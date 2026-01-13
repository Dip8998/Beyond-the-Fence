namespace BTF.Game
{
    using BTF.Enemy;
    using BTF.FirstNB;
    using BTF.Inventory;
    using BTF.Player;
    using BTF.Scenes;
    using BTF.SeconNB;

    public sealed class GameContext
    {
        public PlayerController Player { get; }
        public FirstNeighborController FirstNeighbor { get; }
        public SecondNeighborController SecondNeighbor { get; }
        public EnemyController Boss { get; }
        public InteriorSceneService InteriorService { get; }
        public InventoryController Inventory { get; }

        public GameContext(
            PlayerController player,
            FirstNeighborController firstNeighbor,
            SecondNeighborController secondNeighbor,
            EnemyController boss,
            InteriorSceneService interiorService,
            InventoryController inventory)
        {
            Player = player;
            FirstNeighbor = firstNeighbor;
            SecondNeighbor = secondNeighbor;
            Boss = boss;
            InteriorService = interiorService;
            Inventory = inventory;
        }

    }
}
