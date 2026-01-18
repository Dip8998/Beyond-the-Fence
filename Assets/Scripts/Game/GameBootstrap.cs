using BTF.Boat;
using BTF.Camera;
using BTF.Enemy;
using BTF.Fence;
using BTF.FirstNB;
using BTF.Game;
using BTF.Input;
using BTF.Interaction;
using BTF.Inventory;
using BTF.NPC;
using BTF.Player;
using BTF.Quest;
using BTF.Resource;
using BTF.Scenes;
using BTF.SeconNB;
using BTF.UI;
using BTF.UI.Health;
using BTF.UI.Inventory;
using BTF.UI.Quest;
using BTF.Villager;
using BTF.World;
using UnityEngine;

namespace BTF.Game
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private CameraFollow2D cameraFollow;
        [SerializeField] private InteractionDetector interactionDetector;

        [Header("World Objects")]
        [SerializeField] private EnemyView[] villageEnemies;
        [SerializeField] private TreeView[] treeViews;
        [SerializeField] private BerryView[] berryViews;
        [SerializeField] private WeaponGiverView weaponGiverView;
        [SerializeField] private FenceView fenceView;
        [SerializeField] private VillagerBoatQuestView villagerBoatQuestView;
        [SerializeField] private BoatView boatView;
        [SerializeField] private Transform boatPosition;
        [SerializeField] private GameObject slimeBridge;
        [SerializeField] private GameObject bridgeBlocker;

        [Header("Interiors")]
        [SerializeField] private InteriorEntrance[] interiorEntrances;

        [Header("Startup")]
        [SerializeField] private GameObject worldRoot;
        [SerializeField] private InteriorSceneId startInterior = InteriorSceneId.PlayerHouse;
        [SerializeField] private Transform playerInteriorReturnDummy;

        [Header("UI")]
        [SerializeField] private InventoryUIView inventoryUIView;
        [SerializeField] private PlayerHealthUIView playerHealthUIView;
        [SerializeField] private BerryButtonView berryButtonView;
        [SerializeField] private QuestUIView questUIView;

        private PlayerController playerController;
        private InputService inputService;
        private InteractionSystem interactionSystem;
        private InventoryController inventoryController;
        private InteriorSceneService interiorService;
        private GameContext gameContext;

        private void Awake()
        {
            worldRoot.SetActive(false);

            // ---------- INPUT ----------
            inputService = new InputService();
            inputProvider.Bind(inputService);

            // ---------- PLAYER ----------
            var playerModel = new PlayerModel(100, 5f);
            playerController = new PlayerController(playerModel, inputService);
            playerView.Bind(playerController);
            cameraFollow.SetTarget(playerView.transform);

            // ---------- INTERACTION ----------
            interactionSystem = new InteractionSystem();
            interactionDetector.Bind(interactionSystem);

            // ---------- QUEST ----------
            var questController = new QuestController();
            new QuestUIController(questController, questUIView);

            // ---------- INVENTORY ----------
            var inventoryModel = new InventoryModel();
            inventoryController = new InventoryController(inventoryModel, questController);

            // ---------- GAME CONTEXT ----------
            var firstNeighborController =
                new FirstNeighborController(new FirstNeighborModel());

            var secondNeighborController =
                new SecondNeighborController(new SecondNeighborModel());

            interiorService = new InteriorSceneService(
                playerController,
                playerView,
                null
            );

            gameContext = new GameContext(
                playerController,
                firstNeighborController,
                secondNeighborController,
                boss: null,
                interiorService,
                inventoryController,
                questController
            );

            interiorService.SetContext(gameContext);

            // ---------- WORLD OBJECTS ----------
            foreach (var treeView in treeViews)
            {
                var treeController =
                    new TreeController(new TreeModel(5), inventoryController);
                treeView.Bind(treeController);
            }

            foreach (var berryView in berryViews)
            {
                var berryController =
                    new BerryController(new BerryModel(), inventoryController);
                berryView.Bind(berryController);
            }

            foreach (var enemyView in villageEnemies)
            {
                var enemyController =
                    new EnemyController(new EnemyModel(2f, 3f, 10));

                enemyView.Bind(enemyController, gameContext);

                var detection = enemyView.GetComponentInChildren<EnemyDetection>();
                detection.Bind(playerView.transform, enemyController);
            }

            fenceView.Bind(playerController, gameContext);

            var weaponGiverController = new WeaponGiverController();
            weaponGiverView.Bind(weaponGiverController, playerController, gameContext);

            var bridgeController = new BridgeController(
                inventoryController,
                slimeBridge,
                bridgeBlocker,
                requiredWood: 20
            );

            var villagerBoatQuestController =
                new VillagerBoatQuestController(
                    new VillagerBoatQuestModel(4, 10, 1),
                    inventoryController,
                    playerController,
                    boatView,
                    boatPosition,
                    bridgeController,
                    gameContext
                );

            villagerBoatQuestView.Bind(villagerBoatQuestController);

            // ---------- UI ----------
            _ = new InventoryUIController(
                new InventoryUIModel(),
                inventoryUIView,
                inventoryController
            );

            _ = new PlayerHealthUIController(
                new PlayerHealthUIModel(),
                playerHealthUIView,
                playerController
            );

            var berryButtonController =
                new BerryButtonController(inventoryController, playerController);

            berryButtonView.Bind(
                berryButtonController,
                inventoryController,
                playerController
            );

            // ---------- INTERIORS ----------
            foreach (var entrance in interiorEntrances)
            {
                entrance.Bind(interiorService);
            }
        }

        private void Start()
        {
            playerController.Lock();
            interiorService.OnExitInterior += OnFirstInteriorExit;

            interiorService.Enter(
                startInterior.ToString(),
                playerInteriorReturnDummy.position
            );
        }

        private void Update()
        {
            if (inputService.ConsumeInteractPress())
            {
                interactionSystem.TryInteract();
            }
        }

        private void OnFirstInteriorExit()
        {
            worldRoot.SetActive(true);
            interiorService.OnExitInterior -= OnFirstInteriorExit;
        }
    }
}
