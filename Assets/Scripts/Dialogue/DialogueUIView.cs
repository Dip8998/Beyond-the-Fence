using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BTF.Dialogue
{
    public sealed class DialogueUIView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text speakerText;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private Button continueButton;

        private System.Action onContinue;

        private void Awake()
        {
            panel.SetActive(false);
            continueButton.onClick.AddListener(HandleContinue);
        }

        public void Show(string speaker, string text, System.Action onContinue)
        {
            panel.SetActive(true);
            speakerText.text = speaker;
            dialogueText.text = text;
            this.onContinue = onContinue;
        }

        private void HandleContinue()
        {
            onContinue?.Invoke();
        }

        public void Hide()
        {
            panel.SetActive(false);
            onContinue = null;
        }
    }
}
