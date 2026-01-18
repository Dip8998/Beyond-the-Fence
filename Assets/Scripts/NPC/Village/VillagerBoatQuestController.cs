using BTF.Boat;
using BTF.Game;
using BTF.Inventory;
using BTF.Player;
using BTF.World;
using UnityEngine;

namespace BTF.Villager
{
    public class VillagerBoatQuestController
    {
        private readonly VillagerBoatQuestModel model;
        private readonly InventoryController inventory;
        private readonly PlayerController player;

        private readonly BoatView boatPrefab;
        private readonly Transform boatSpawnPoint;
        private readonly BridgeController bridgeController;
        private GameContext gameContext;

        public VillagerBoatQuestController(
            VillagerBoatQuestModel model,
            InventoryController inventory,
            PlayerController player,
            BoatView boatPrefab,
            Transform boatSpawnPoint,
            BridgeController bridgeController,
            GameContext gameContext)
        {
            this.model = model;
            this.inventory = inventory;
            this.player = player;
            this.boatPrefab = boatPrefab;
            this.boatSpawnPoint = boatSpawnPoint;
            this.bridgeController = bridgeController;
            this.gameContext = gameContext;
        }

        public VillagerBoatQuestState GetState() => model.State;

        public void TryActivate()
        {
            if (!GameProgress.IsFenceUnlocked)
                return;

            if (model.State == VillagerBoatQuestState.Inactive)
            {
                model.Activate();
                Debug.Log("Villager: My son was taken...");
            }
        }

        public void Interact(Transform bossIslandPoint)
        {
            switch (model.State)
            {
                case VillagerBoatQuestState.Active:
                    Debug.Log("Villager: My son was taken! Bring me 10 wood.");
                    model.StartQuest();
                    break;

                case VillagerBoatQuestState.InProgress:
                    HandleProgress(bossIslandPoint);
                    break;

                case VillagerBoatQuestState.BoatReady:
                    Debug.Log("Villager: Take the boat and save my son!");
                    model.Complete();
                    break;
            }
        }

        private void HandleProgress(Transform bossIslandPoint)
        {
            if (!model.HasRealizedGear)
            {
                if (!inventory.HasWood(model.RequiredBoatWood))
                {
                    Debug.Log("Villager: I need 10 wood to build the boat.");
                    return;
                }

                inventory.ConsumeWood(model.RequiredBoatWood);
                model.RealizeGear();

                Debug.Log("Villager: Wait… I forgot something. The boat needs gear.");
                Debug.Log("Villager: We need a bridge to reach the slime island.");

                return;
            }

            if (!bridgeController.IsBuilt)
            {
                if (!inventory.HasWood(model.RequiredBridgeWood))
                {
                    Debug.Log("Villager: Bring 20 wood to build the bridge.");
                    return;
                }

                bridgeController.Build();
                Debug.Log("Villager: The bridge is ready. Go to the slime island!");
                return;
            }

            TryBuildBoat(bossIslandPoint);
        }


        private void TryBuildBoat(Transform bossIslandPoint)
        {
            if (!inventory.HasGear(model.RequiredGear))
            {
                Debug.Log("Villager: You still need the gear from the slime boss.");
                return;
            }

            BoatView boat = Object.Instantiate(
                boatPrefab,
                boatSpawnPoint.position,
                Quaternion.identity
            );

            boat.Bind(player, bossIslandPoint);

            Debug.Log("Villager: The boat is ready!");
            model.BoatReady();
            gameContext.Quest.Advance();
        }
    }
}
