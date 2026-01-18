using BTF.Game;
using BTF.Interfaces;
using UnityEngine;

namespace BTF.Villager
{
    public class VillagerBoatQuestView : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform bossIslandPoint;

        private VillagerBoatQuestController controller;
        private GameContext gameContext;

        public void Bind(VillagerBoatQuestController controller, GameContext gameContext)
        {
            this.controller = controller;
            this.gameContext = gameContext;
        }

        public void Interact()
        {
            controller.TryActivate();

            if (controller.GetState() == VillagerBoatQuestState.Inactive)
            {
                Debug.Log("Villager ignores you.");
                return;
            }

            gameContext.Quest.CompleteTask(0);

            controller.Interact(bossIslandPoint);
        }
    }
}
