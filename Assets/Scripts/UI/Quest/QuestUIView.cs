using BTF.Quest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BTF.UI.Quest
{
    public sealed class QuestUIView : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button expandButton;
        [SerializeField] private TMP_Text arrowText;

        [Header("Body")]
        [SerializeField] private GameObject descriptionPanel;
        [SerializeField] private TMP_Text descriptionText;

        [Header("Tasks")]
        [SerializeField] private Transform taskRoot;
        [SerializeField] private TMP_Text taskPrefab;

        private bool expanded;

        private void Awake()
        {
            expandButton.onClick.AddListener(Toggle);
            Collapse();
        }

        public void Render(QuestModel quest)
        {
            titleText.text = quest.Title;
            descriptionText.text = quest.Description;

            foreach (Transform child in taskRoot)
                Destroy(child.gameObject);

            if (quest.Tasks != null)
            {
                foreach (var task in quest.Tasks)
                {
                    var t = Instantiate(taskPrefab, taskRoot);
                    t.text = task.Completed
                        ? $"✓ {task.Text}"
                        : $"• {task.Text}";
                }
            }

            Collapse(); 
        }

        private void Toggle()
        {
            if (expanded)
                Collapse();
            else
                Expand();
        }

        private void Expand()
        {
            expanded = true;
            arrowText.text = "v";
            descriptionPanel.SetActive(true);
        }

        private void Collapse()
        {
            expanded = false;
            arrowText.text = ">";
            descriptionPanel.SetActive(false);
        }
    }
}
