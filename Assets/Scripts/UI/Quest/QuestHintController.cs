using UnityEngine;
using BTF.Discovery;

namespace BTF.UI.Quest
{
    public sealed class QuestHintController : MonoBehaviour
    {
        [SerializeField] private float delay = 5f;
        [SerializeField] private QuestButtonGlow questButtonGlow;

        private DiscoveryController discovery;
        private bool shown;

        public void Bind(DiscoveryController discovery)
        {
            this.discovery = discovery;
            Invoke(nameof(ShowHint), delay);
        }

        private void ShowHint()
        {
            if (shown) return;

            shown = true;

            questButtonGlow.StartGlow();
            discovery.Notify("Check your quest list");
        }

        public void OnQuestOpened()
        {
            questButtonGlow.StopGlow();
        }
    }
}