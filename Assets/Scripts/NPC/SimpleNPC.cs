using BTF.Interfaces;
using UnityEngine;

namespace BTF.NPC
{
    public sealed class SimpleNPC : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Npc Interacted");
        }
    }
}
