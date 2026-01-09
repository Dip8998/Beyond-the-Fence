using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.Boat
{
    public class BoatView : MonoBehaviour, IInteractable
    {
        [SerializeField] private float travelSpeed = 2f;
        [SerializeField] private Transform seatPoint;

        private BoatController controller;
        private BoatModel model;
        private PlayerController player;
        private Transform bossIslandPoint;

        public void Bind(PlayerController player, Transform bossIslandPoint)
        {
            this.player = player;
            this.bossIslandPoint = bossIslandPoint;

            model = new BoatModel();
            controller = new BoatController(
                model,
                transform,
                bossIslandPoint,
                travelSpeed
            );
        }

        public void Interact()
        {
            if (controller == null || player == null)
            {
                Debug.LogError("BoatView not bound properly!");
                return;
            }

            controller.Board(player, seatPoint);
        }

        private void Update()
        {
            controller?.Tick();
        }
    }
}
