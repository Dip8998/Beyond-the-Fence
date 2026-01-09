using BTF.Boat;
using BTF.Inventory;
using BTF.Player;
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

        public VillagerBoatQuestController(
            VillagerBoatQuestModel model,
            InventoryController inventory,
            PlayerController player,
            BoatView boatPrefab,
            Transform boatSpawnPoint)
        {
            this.model = model;
            this.inventory = inventory;
            this.player = player;
            this.boatPrefab = boatPrefab;
            this.boatSpawnPoint = boatSpawnPoint;
        }

        public VillagerBoatQuestState GetState() => model.State;

        public void TryActivate()
        {
            if (!player.HasFenceKey())
                return;

            if (model.State == VillagerBoatQuestState.Inactive)
            {
                model.Activate();
                Debug.Log("Villager quest activated");
            }
        }

        public void Interact(Transform bossIslandPoint)
        {
            switch (model.State)
            {
                case VillagerBoatQuestState.Active:
                    Debug.Log("Villager: My son was taken... I need wood & gear.");
                    model.StartQuest();
                    break;

                case VillagerBoatQuestState.InProgress:
                    TryBuildBoat(bossIslandPoint);
                    break;

                case VillagerBoatQuestState.BoatReady:
                    Debug.Log("Villager: Take the boat and save my son!");
                    model.Complete();
                    break;
            }
        }

        private void TryBuildBoat(Transform bossIslandPoint)
        {
            Debug.Log($"Checking materials: Wood {inventory.GetWoodCount()}/{model.RequiredWood}");

            if (!inventory.HasWood(model.RequiredWood) /*||
                !inventory.HasGear(model.RequiredGear)*/)
            {
                Debug.Log("Villager: You still need materials.");
                return;
            }

            inventory.ConsumeWood(model.RequiredWood);
            inventory.ConsumeGear(model.RequiredGear);

            BoatView boat = Object.Instantiate(
                boatPrefab,
                boatSpawnPoint.position,
                Quaternion.identity
            );

            boat.Bind(player, bossIslandPoint);


            Debug.Log("Boat Created!");
            model.BoatReady();
        }
    }
}
