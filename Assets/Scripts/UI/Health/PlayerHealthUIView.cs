using UnityEngine;
using UnityEngine.UI;

namespace BTF.UI.Health
{
    public sealed class PlayerHealthUIView : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void SetFill(float value)
        {
            fillImage.fillAmount = Mathf.Clamp01(value);
        }
    }
}
