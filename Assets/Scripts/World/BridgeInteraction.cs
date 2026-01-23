using BTF.Game;
using BTF.Player;
using UnityEngine;

namespace BTF.World
{
    public class BridgeInteraction : MonoBehaviour
    {
        private GameContext context;

        public void Bind(GameContext gameContext)
        {
            context = gameContext;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerView>(out var player))
            {
                context.Discovery.Notify("You need a bridge to cross this area.");
            }
        }
    }
}
