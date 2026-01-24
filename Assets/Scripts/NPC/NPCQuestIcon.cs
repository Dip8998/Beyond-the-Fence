using UnityEngine;

namespace BTF.NPC
{
    public sealed class NPCQuestIcon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer iconRenderer;

        private void Awake()
        {
            iconRenderer.enabled = false;
        }

        public void Show()
        {
            iconRenderer.enabled = true;
        }

        public void Hide()
        {
            iconRenderer.enabled = false;
        }
    }
}