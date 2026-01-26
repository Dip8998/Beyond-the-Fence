using BTF.Boat;
using BTF.Game;
using BTF.Inventory;
using BTF.Player;
using BTF.World;
using UnityEngine;

namespace BTF.Villager
{
    public sealed class VillagerBoatQuestController
    {
        private readonly VillagerBoatQuestModel model;
        private readonly InventoryController inventory;
        private readonly PlayerController player;
        private readonly BoatView boatPrefab;
        private readonly Transform boatSpawnPoint;
        private readonly BridgeController bridgeController;
        private readonly GameContext context;

        public VillagerBoatQuestModel GetModel() => model;

        public VillagerBoatQuestController(
            VillagerBoatQuestModel model,
            InventoryController inventory,
            PlayerController player,
            BoatView boatPrefab,
            Transform boatSpawnPoint,
            BridgeController bridgeController,
            GameContext context)
        {
            this.model = model;
            this.inventory = inventory;
            this.player = player;
            this.boatPrefab = boatPrefab;
            this.boatSpawnPoint = boatSpawnPoint;
            this.bridgeController = bridgeController;
            this.context = context;
        }

        public VillagerBoatQuestState GetState() => model.State;

        public void TryActivate()
        {
            if (!GameProgress.IsFenceUnlocked)
                return;

            if (model.State == VillagerBoatQuestState.Inactive)
                model.Activate();
        }

        public void TryProgress(Transform bossIslandPoint)
        {
            if (model.State != VillagerBoatQuestState.InProgress)
                return;

            if (!model.HasRealizedGear)
            {
                if (inventory.HasWood(model.RequiredBoatWood))
                {
                    inventory.ConsumeWood(model.RequiredBoatWood);
                    model.RealizeGear();
                    context.Quest.CompleteTask(1); 
                }
                return;
            }

            if (!model.IsBridgeBuilt)
            {
                if (!inventory.HasWood(model.RequiredBridgeWood))
                    return;

                inventory.ConsumeWood(model.RequiredBridgeWood);
                bridgeController.Build();
                context.Discovery.Notify("Bridge to the island built!");
                model.MarkBridgeBuilt();          
                context.Quest.CompleteTask(2);

                return;
            }

            if (inventory.HasGear(model.RequiredGear))
            {
                context.Quest.CompleteTask(3); 
                BuildBoat(bossIslandPoint);
            }
        }

        private void BuildBoat(Transform bossIslandPoint)
        {
            BoatView boat = Object.Instantiate(
                boatPrefab,
                boatSpawnPoint.position,
                Quaternion.identity
            );

            boat.Bind(player, bossIslandPoint);

            model.BoatReady();
            context.Quest.CompleteTask(4); 
        }

        public void OnFirstConversationFinished()
        {
            if (model.State != VillagerBoatQuestState.Active)
                return;

            model.StartQuest();
            context.Quest.CompleteTask(0);
        }
    }
}
