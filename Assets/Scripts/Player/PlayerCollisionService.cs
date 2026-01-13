using UnityEngine;

namespace BTF.Player
{
    public sealed class PlayerCollisionService
    {
        private readonly int playerLayer;
        private readonly int villageLayer;
        private readonly int interiorLayer;

        public PlayerCollisionService()
        {
            playerLayer = LayerMask.NameToLayer("Player");
            villageLayer = LayerMask.NameToLayer("Village");
            interiorLayer = LayerMask.NameToLayer("Interior");
        }

        public void EnterInterior()
        {
            Physics2D.IgnoreLayerCollision(playerLayer, villageLayer, true);
            Physics2D.IgnoreLayerCollision(playerLayer, interiorLayer, false);
        }

        public void ExitInterior()
        {
            Physics2D.IgnoreLayerCollision(playerLayer, villageLayer, false);
            Physics2D.IgnoreLayerCollision(playerLayer, interiorLayer, true);
        }
    }
}
