using BTF.Game;
using BTF.Player;

namespace BTF.NPC
{
    public class WeaponGiverController
    {
        public void GiveWeapon(PlayerController player, GameContext context)
        {
            if (player.HasWeapon)
                return;

            player.ReceiveWeapon();

            context.Quest.Advance();
        }
    }
}