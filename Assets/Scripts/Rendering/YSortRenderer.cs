using UnityEngine;

namespace BTF.Rendering
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class YSortRenderer : MonoBehaviour
    {
        [Tooltip("Higher value = renders more in front")]
        [SerializeField] private int sortingOffset = 0;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            spriteRenderer.sortingOrder =
                Mathf.RoundToInt(-(transform.position.y * 100)) + sortingOffset;
        }
    }
}
