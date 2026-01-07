using BTF.Game;
using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.FirstNB
{
    public class FirstNeighborView : MonoBehaviour, IInteractable
    {
        private FirstNeighborController controller;
        private PlayerController playerController;

        public void Bind(FirstNeighborController controller, PlayerController playerController)
        {
            this.controller = controller;
            this.playerController = playerController;
        }

        public void Interact()
        {
            switch (controller.GetState())
            {
                case FirstNeighborState.Idle:
                    Debug.Log("Please help me find my jewelry box...");
                    controller.OnPlayerInteract();
                    break;

                case FirstNeighborState.AskedForHelp:
                    Debug.Log("Did you find my jewelry box?");
                    break;

                case FirstNeighborState.ConflictResolved:
                    Debug.Log("Thank you! Take this key and unlock the fence.");
                    controller.GiveKey(playerController);
                    GameProgress.IsFenceUnlocked = true;
                    break;
            }
        }
    }
}
