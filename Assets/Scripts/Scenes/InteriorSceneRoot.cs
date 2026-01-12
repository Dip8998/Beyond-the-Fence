using UnityEngine;

namespace BTF.Scenes
{
    public sealed class InteriorSceneRoot : MonoBehaviour, IInteriorScene
    {
        [SerializeField] private Transform playerSpawn;

        public Transform PlayerSpawn => playerSpawn;
    }
}
