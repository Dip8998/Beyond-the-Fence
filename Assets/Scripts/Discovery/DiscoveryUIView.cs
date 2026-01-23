using UnityEngine;

namespace BTF.Discovery
{
    public sealed class DiscoveryUIView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMPro.TMP_Text text;
        [SerializeField] private float duration = 2f;

        private Coroutine routine;

        private void Awake()
        {
            panel.SetActive(false);
        }

        public void Show(string message)
        {
            if (routine != null)
                StopCoroutine(routine);

            text.text = message;
            panel.SetActive(true);

            routine = StartCoroutine(HideRoutine());
        }

        private System.Collections.IEnumerator HideRoutine()
        {
            yield return new UnityEngine.WaitForSeconds(duration);
            panel.SetActive(false);
            routine = null;
        }
    }
}
