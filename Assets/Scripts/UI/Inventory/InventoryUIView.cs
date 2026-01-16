using UnityEngine;
using TMPro;

namespace BTF.UI.Inventory
{
    public sealed class InventoryUIView : MonoBehaviour
    {
        [Header("Empty State")]
        [SerializeField] private TMP_Text emptyText;

        [Header("Items")]
        [SerializeField] private GameObject woodBlock;
        [SerializeField] private TMP_Text woodText;

        [SerializeField] private GameObject gearBlock;
        [SerializeField] private TMP_Text gearText;

        public void ShowEmpty()
        {
            emptyText.gameObject.SetActive(true);
            woodBlock.SetActive(false);
            gearBlock.SetActive(false);
        }

        public void ShowItems(int wood, int gear)
        {
            emptyText.gameObject.SetActive(false);

            woodBlock.SetActive(wood > 0);
            gearBlock.SetActive(gear > 0);

            if (wood > 0)
                woodText.text = $"{wood}";

            if (gear > 0)
                gearText.text = $"{gear}";
        }
    }
}
