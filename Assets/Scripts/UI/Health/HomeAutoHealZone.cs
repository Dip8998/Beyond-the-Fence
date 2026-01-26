using System.Collections;
using UnityEngine;
using BTF.Player;
using BTF.Discovery;
using BTF.Scenes;
using BTF.Game;

namespace BTF.Interiors
{
    public sealed class HomeAutoHealZone
        : MonoBehaviour, ISceneBinder
    {
        [Header("Timing")]
        [SerializeField] private float startDelay = 2f;
        [SerializeField] private float healInterval = 0.25f;
        [SerializeField] private int healPerTick = 1;

        private DiscoveryController discovery;
        private Coroutine healRoutine;
        private bool notified;

        public void Bind(GameContext context)
        {
            discovery = context.Discovery;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerView>(out var view))
                return;

            if (!notified)
            {
                discovery.Notify("You feel safe here...");
                notified = true;
            }

            if (healRoutine != null)
                StopCoroutine(healRoutine);

            healRoutine = StartCoroutine(
                HealGradually(view.Controller)
            );
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            StopHealing();
        }

        private void StopHealing()
        {
            if (healRoutine != null)
            {
                StopCoroutine(healRoutine);
                healRoutine = null;
            }

            notified = false;
        }

        private IEnumerator HealGradually(PlayerController player)
        {
            yield return new WaitForSeconds(startDelay);

            while (player.GetCurrentHP() < player.GetMaxHP())
            {
                player.Heal(healPerTick);
                yield return new WaitForSeconds(healInterval);
            }

            healRoutine = null;
        }
    }
}