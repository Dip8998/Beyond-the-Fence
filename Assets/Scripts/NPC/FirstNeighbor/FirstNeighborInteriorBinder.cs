using BTF.FirstNB;
using BTF.Game;
using BTF.Scenes;
using UnityEngine;

namespace BTF.NPC
{
    public sealed class FirstNeighborInteriorBinder : MonoBehaviour, ISceneBinder
    {
        [SerializeField] private FirstNeighborView view;

        public void Bind(GameContext context)
        {
            view.Bind(context.FirstNeighbor, context.Player);
        }
    }
}
