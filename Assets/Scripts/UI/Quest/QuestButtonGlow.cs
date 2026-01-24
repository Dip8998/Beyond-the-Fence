using UnityEngine;
using UnityEngine.UI;

namespace BTF.UI.Quest
{
    public sealed class QuestButtonGlow : MonoBehaviour
    {
        [SerializeField] private float scaleAmount = 1.1f;
        [SerializeField] private float pulseSpeed = 3f;
        [SerializeField] private Color glowColor = new Color(1f, 0.9f, 0.3f);

        private RectTransform rect;
        private Image image;

        private Vector3 baseScale;
        private Color baseColor;

        private bool glowing;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            image = GetComponent<Image>();

            baseScale = rect.localScale;
            baseColor = image.color;
        }

        public void StartGlow()
        {
            glowing = true;
        }

        public void StopGlow()
        {
            glowing = false;
            rect.localScale = baseScale;
            image.color = baseColor;
        }

        private void Update()
        {
            if (!glowing) return;

            float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;

            rect.localScale =
                Vector3.Lerp(baseScale, baseScale * scaleAmount, t);

            image.color =
                Color.Lerp(baseColor, glowColor, t);
        }
    }
}