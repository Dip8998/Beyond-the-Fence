using BTF.Interfaces;
using BTF.Scenes;
using UnityEngine;

namespace BTF.Interaction
{
    public sealed class InteriorExit : MonoBehaviour, IInteractable
    {
        private InteriorSceneService sceneService;

        public void Bind(InteriorSceneService service)
        {
            sceneService = service;
        }

        public void Interact()
        {
            sceneService.Exit();
        }
    }
}
