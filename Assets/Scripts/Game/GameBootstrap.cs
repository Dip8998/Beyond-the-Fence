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

        [Header("World Objects (Village Only)")]
        [SerializeField] private EnemyView[] villageEnemies;
        [SerializeField] private TreeView treeView;
        [SerializeField] private BerryView berryView;
        [SerializeField] private WeaponGiverView weaponGiverView;
        [SerializeField] private FenceView fenceView;
        [SerializeField] private VillagerBoatQuestView villagerBoatQuestView;
        [SerializeField] private BoatView boatView;
        [SerializeField] private Transform boatPosition;
        [SerializeField] private GameObject slimeBridge;
        [SerializeField] private GameObject bridgeBlocker;

        [Header("Interiors")]
        [SerializeField] private InteriorEntrance[] interiorEntrances;

        private PlayerController playerController;
        private InputService inputService;
        private InteractionSystem interactionSystem;

        private InventoryController inventoryController;

        private EnemyController bossController;
        private FirstNeighborController firstNeighborController;
        private SecondNeighborController secondNeighborController;

        private GameContext gameContext;
        private InteriorSceneService interiorService;

        public InteriorSceneService InteriorService => interiorService;

        private void Awake()
        {
            Debug.Assert(playerView != null, "PlayerView is NULL");
            Debug.Assert(inputProvider != null, "InputProvider is NULL");
            Debug.Assert(cameraFollow != null, "CameraFollow2D is NULL");
            Debug.Assert(interactionDetector != null, "InteractionDetector is NULL");

            inputService = new InputService();
            inputProvider.Bind(inputService);

            var playerModel = new PlayerModel(100, 5f);
            playerController = new PlayerController(playerModel, inputService);
            playerView.Bind(playerController);
            cameraFollow.SetTarget(playerView.transform);

            interactionSystem = new InteractionSystem();
            interactionDetector.Bind(interactionSystem);

            var inventoryModel = new InventoryModel();
            inventoryController = new InventoryController(inventoryModel);

            var treeModel = new TreeModel(5);
            var treeController = new TreeController(treeModel, inventoryController);
            treeView.Bind(treeController);

            var berryModel = new BerryModel();
            var berryController = new BerryController(berryModel, inventoryController, playerController);
            berryView.Bind(berryController);

            foreach (var enemyView in villageEnemies)
            {
                var model = new EnemyModel(
                    moveSpeed: 2f,
                    chaseSpeed: 3f,
                    maxHP: 10
                );

                var controller = new EnemyController(model);

                enemyView.Bind(controller);

                var detection = enemyView.GetComponentInChildren<EnemyDetection>();
                detection.Bind(playerView.transform, controller);
            }

            var firstNeighborModel = new FirstNeighborModel();
            firstNeighborController = new FirstNeighborController(firstNeighborModel);

            var secondNeighborModel = new SecondNeighborModel();
            secondNeighborController = new SecondNeighborController(secondNeighborModel);

            fenceView.Bind(playerController);

            var weaponGiverController = new WeaponGiverController();
            weaponGiverView.Bind(weaponGiverController, playerController);

            var bridgeController = new BridgeController(
                inventoryController,
                slimeBridge,
                bridgeBlocker,
                requiredWood: 20
            );

            var villagerBoatQuestModel = new VillagerBoatQuestModel(4, 10, 1);
            var villagerBoatQuestController = new VillagerBoatQuestController(
                villagerBoatQuestModel,
                inventoryController,
                playerController,
                boatView,
                boatPosition,
                bridgeController
            );
            villagerBoatQuestView.Bind(villagerBoatQuestController);

            interiorService = new InteriorSceneService(
                playerController,
                playerView,
                null // temporary
            );

            gameContext = new GameContext(
                playerController,
                firstNeighborController,
                secondNeighborController,
                bossController,
                interiorService,
                inventoryController
            );

            interiorService.SetContext(gameContext);


            foreach (var entrance in interiorEntrances)
            {
                entrance.Bind(interiorService);
            }
        }

        private void Update()
        {
            if (inputService.ConsumeInteractPress())
            {
                interactionSystem.TryInteract();
            }
        }
    }
}
