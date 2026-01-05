using BTF.Camera;
using BTF.Enemy;
using BTF.Input;
using BTF.Interaction;
using BTF.Inventory;
using BTF.Player;
using BTF.Quest;
using BTF.Resource;
using UnityEngine;

namespace BTF.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private CameraFollow2D cameraFollow;
        [SerializeField] private InteractionDetector interactionDetector;
        [SerializeField] private EnemyView enemyView;
        [SerializeField] private EnemyDetection enemyDetection;
        [SerializeField] private TreeView treeView;
        [SerializeField] private BerryView berryView;
        [SerializeField] private NPCQuestView npcQuestView;

        private PlayerModel playerModel;
        private PlayerController playerController;
        private InputService inputService;
        private InteractionSystem interactionSystem;
        private EnemyModel enemyModel;
        private EnemyController enemyController;
        private TreeModel treeModel;
        private TreeController treeController;
        private BerryModel berryModel;
        private BerryController berryController;
        private InventoryModel inventoryModel;
        private InventoryController inventoryController;
        private NPCQuestModel npcQuestModel;
        private NPCQuestController npcQuestController;

        private void Awake()
        {
            Debug.Assert(playerView != null);
            Debug.Assert(inputProvider != null);
            Debug.Assert(cameraFollow != null);

            playerModel = new PlayerModel(100, 5f);
            inputService = new InputService();
            inputProvider.Bind(inputService);

            playerController = new PlayerController(playerModel, inputService);
            playerView.Bind(playerController);
            cameraFollow.SetTarget(playerView.transform);

            interactionSystem = new InteractionSystem();    
            interactionDetector.Bind(interactionSystem);

            enemyModel = new EnemyModel(2f,3f,10);
            enemyController = new EnemyController(enemyModel);
            enemyView.Bind(enemyController);
            enemyDetection.Bind(playerView.transform, enemyController);

            inventoryModel = new InventoryModel();
            inventoryController = new InventoryController(inventoryModel);

            treeModel = new TreeModel(5);
            treeController = new TreeController(treeModel, inventoryController);
            treeView.Bind(treeController);

            berryModel = new BerryModel();
            berryController = new BerryController(berryModel, inventoryController);
            berryView.Bind(berryController);

            npcQuestModel = new NPCQuestModel(3);
            npcQuestController = new NPCQuestController(npcQuestModel, inventoryController);
            npcQuestView.Bind(npcQuestController);
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
