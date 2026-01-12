namespace BTF.Game
{
    using BTF.Player;
    using BTF.FirstNB;
    using BTF.SeconNB;
    using BTF.Enemy;
    using BTF.Scenes;

    public sealed class GameContext
    {
        public PlayerController Player { get; }
        public FirstNeighborController FirstNeighbor { get; }
        public SecondNeighborController SecondNeighbor { get; }
        public EnemyController Boss { get; }
        public InteriorSceneService InteriorService { get; }

        public GameContext(
            PlayerController player,
            FirstNeighborController firstNeighbor,
            SecondNeighborController secondNeighbor,
            EnemyController boss,
            InteriorSceneService interiorService)
        {
            Player = player;
            FirstNeighbor = firstNeighbor;
            SecondNeighbor = secondNeighbor;
            Boss = boss;
            InteriorService = interiorService;
        }
    }
}
