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

        public void Bind(PlayerController player)
        {
            this.player = player;
        }

        public void Interact()
        {
            if (!player.HasFenceKey())
            {
                Debug.Log("Fence is locked.");
                return;
            }

            Unlock();
        }

        private void Unlock()
        {
            Debug.Log("Fence unlocked!");
            fenceCollider.enabled = false;
            fenceVisual.SetActive(false);
        }
    }
}
