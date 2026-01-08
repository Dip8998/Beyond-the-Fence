using BTF.Interfaces;
using UnityEngine;

namespace BTF.Boat
{
    public class BoatView : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Boat: Sailing to boss island...");
        }
    }
}