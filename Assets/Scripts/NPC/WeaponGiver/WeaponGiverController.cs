using BTF.Game;
using BTF.Player;

namespace BTF.NPC
{
    public class WeaponGiverController
    {
        private bool weaponGiven;

        public bool HasGivenWeapon => weaponGiven;

        public void GiveWeapon(PlayerController player, GameContext context)
        {
            if (weaponGiven) return;

            player.ReceiveWeapon();
            weaponGiven = true;
            context.Quest.Advance();
        }
    }
}
