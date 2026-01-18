using UnityEngine;
using UnityEngine.UI;

namespace BTF.UI.Quest
{
    public sealed class ButtonPanelToggle : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button button;

        private bool isOpen;

        private void Awake()
        {
            button.onClick.AddListener(Toggle);
            panel.SetActive(false);
            isOpen = false;
        }

        private void Toggle()
        {
            isOpen = !isOpen;
            panel.SetActive(isOpen);
        }
    }
}
