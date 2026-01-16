using BTF.Interfaces;
using UnityEngine;

namespace BTF.Villager
{
    public class VillagerSonView : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Thankyou for saving me!");
        }
    }
}
