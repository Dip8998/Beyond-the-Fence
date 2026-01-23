using BTF.Dialogue;
using BTF.Discovery;
using BTF.Game;
using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

namespace BTF.NPC
{
    public sealed class WeaponGiverView : MonoBehaviour, IInteractable
    {
        private WeaponGiverController controller;
        private PlayerController player;
        private GameContext context;
        private OldManDialogue dialogue;

        public void Bind(
            WeaponGiverController controller,
            PlayerController player,
            GameContext context)
        {
            this.controller = controller;
            this.player = player;
            this.context = context;

            dialogue = new OldManDialogue();
        }

        public void Interact()
        {
            if (context.DialogueController.IsPlaying)
                return;

            context.DialogueController.StartDialogue(
                context.DialogueRunner.Run(dialogue),
                () =>
                {
                    if (!GameProgress.IsFenceUnlocked)
                        return;

                    if (!controller.HasGivenWeapon)
                        controller.GiveWeapon(player, context);

                    context.Discovery.TryDiscover(DiscoverableItem.Sword);
                }
            );
        }
    }
}
