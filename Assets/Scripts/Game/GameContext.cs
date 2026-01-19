namespace BTF.Game
{
    using BTF.Dialogue;
    using BTF.Enemy;
    using BTF.FirstNB;
    using BTF.Inventory;
    using BTF.Player;
    using BTF.Quest;
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
        public QuestController Quest { get; }
        public DialogueRunner DialogueRunner { get; }
        public DialogueController DialogueController { get; }

        public GameContext(
            PlayerController player,
            FirstNeighborController firstNeighbor,
            SecondNeighborController secondNeighbor,
            EnemyController boss,
            InteriorSceneService interiorService,
            InventoryController inventory,
            QuestController quest,
            DialogueRunner dialogueRunner,
            DialogueController dialogueController)
        {
            Player = player;
            FirstNeighbor = firstNeighbor;
            SecondNeighbor = secondNeighbor;
            Boss = boss;
            InteriorService = interiorService;
            Inventory = inventory;
            Quest = quest;
            DialogueRunner = dialogueRunner;
            DialogueController = dialogueController;
        }

    }
}
