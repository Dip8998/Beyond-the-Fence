using BTF.Game;
using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.Fence
{
    public class FenceView : MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider2D fenceCollider;
        [SerializeField] private GameObject fenceVisual;

        private PlayerController player;
        private GameContext gameContext;

        public void Bind(PlayerController player, GameContext gameContext)
        {
            this.player = player;
            this.gameContext = gameContext;
        }

        public void Interact()
        {
            if (!player.HasFenceKey())
            {
                gameContext.Discovery.Notify("The fence is locked");
                return;
            }

            Unlock();
        }

        private void Unlock()
        {
            Debug.Log("Fence unlocked!");
            GameProgress.IsFenceUnlocked = true;
            fenceCollider.enabled = false;
            fenceVisual.SetActive(false);
            gameContext.Quest.Advance();
        }
    }
}
