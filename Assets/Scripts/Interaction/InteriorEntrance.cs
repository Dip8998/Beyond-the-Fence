using BTF.Interfaces;
using BTF.Scenes;
using UnityEngine;

namespace BTF.Interaction
{
    public sealed class InteriorEntrance : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteriorSceneId sceneId;
        [SerializeField] private Transform returnPoint;

        private InteriorSceneService sceneService;

        public void Bind(InteriorSceneService service)
        {
            sceneService = service;
        }

        public void Interact()
        {
            sceneService.Enter(sceneId.ToString(), returnPoint.position);
        }
    }
}
