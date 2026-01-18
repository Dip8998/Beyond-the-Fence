using UnityEngine;
using BTF.SeconNB;
using BTF.Game;
using BTF.Scenes;

namespace BTF.NPC
{
    public sealed class SecondNeighborInteriorBinder : MonoBehaviour, ISceneBinder
    {
        [SerializeField] private SecondNeighborView view;

        public void Bind(GameContext context)
        {
            view.Bind(context.SecondNeighbor, context.FirstNeighbor,context);
        }
    }
}
