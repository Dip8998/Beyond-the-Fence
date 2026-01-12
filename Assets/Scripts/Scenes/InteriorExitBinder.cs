using UnityEngine;
using BTF.Scenes;
using BTF.Interaction;
using BTF.Game;

namespace BTF.Scenes
{
    public sealed class InteriorExitBinder : MonoBehaviour, ISceneBinder
    {
        [SerializeField] private InteriorExit interiorExit;

        public void Bind(GameContext context)
        {
            interiorExit.Bind(context.InteriorService);
        }
    }
}
