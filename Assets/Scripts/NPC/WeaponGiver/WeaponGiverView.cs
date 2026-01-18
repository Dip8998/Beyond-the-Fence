using BTF.Game;
using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.NPC
{
    public class WeaponGiverView : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Blacksmith";

        private WeaponGiverController controller;
        private PlayerController player;
        private GameContext context;

        public void Bind(WeaponGiverController controller, PlayerController player, GameContext gameContext)
        {
            this.controller = controller;
            this.player = player;
            context = gameContext;
        }

        public void Interact()
        {
            if (!GameProgress.IsFenceUnlocked)
            {
                Debug.Log($"{npcName}: It’s too dangerous outside. Come back after the fence is unlocked.");
                return;
            }

            if (!controller.HasGivenWeapon)
            {
                Debug.Log($"{npcName}: Take this sword. You’ll need it.");
                controller.GiveWeapon(player, context);
            }
            else
            {
                Debug.Log($"{npcName}: I can upgrade your weapon later.");
            }
        }
    }
}
