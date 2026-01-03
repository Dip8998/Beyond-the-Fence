using BTF.Interfaces;
using System.Collections;
using UnityEngine;

namespace BTF.Resource
{
    public class TreeView : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject fullTree;
        [SerializeField] private GameObject cutTree;
        [SerializeField] private SpriteRenderer treeRenderer;
        [SerializeField] private float regrowTime = 5f;

        private TreeController controller;

        public void Bind(TreeController controller)
        {
            this.controller = controller;
            controller.OnTreeCut += CutVisual;
            controller.OnTreeRegrow += RegrowVisual;
        }

        public void TakeDamage(int damage) => controller?.TakeDamage(damage);

        private void CutVisual()
        {
            fullTree.SetActive(false);
            cutTree.SetActive(true);
            StartCoroutine(RegrowRoutine());
        }

        private IEnumerator RegrowRoutine()
        {
            yield return new WaitForSeconds(regrowTime);
            controller?.Regrow();
        }

        private void RegrowVisual()
        {
            fullTree.SetActive(true);
            StartCoroutine(FadeInTree());
            cutTree.SetActive(false);
        }

        private IEnumerator FadeInTree()
        {
            Color color = treeRenderer.color;
            color.a = 0f;
            treeRenderer.color = color;

            float fadeDuration = 0.8f;
            float t = 0f;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
                treeRenderer.color = color;
                yield return null;
            }

            color.a = 1f;
            treeRenderer.color = color;
        }
    }
}
