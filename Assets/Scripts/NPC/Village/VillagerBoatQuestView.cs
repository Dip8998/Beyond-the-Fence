using BTF.Interfaces;
using UnityEngine;

namespace BTF.Villager
{
    public class VillagerBoatQuestView : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform bossIslandPoint;

        private VillagerBoatQuestController controller;

        public void Bind(VillagerBoatQuestController controller)
        {
            this.controller = controller;
        }

        public void Interact()
        {
            controller.TryActivate();

            if (controller.GetState() == VillagerBoatQuestState.Inactive)
            {
                Debug.Log("Villager ignores you.");
                return;
            }

            controller.Interact(bossIslandPoint);
        }
    }
}
