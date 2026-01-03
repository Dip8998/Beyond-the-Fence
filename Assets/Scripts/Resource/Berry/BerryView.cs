using BTF.Interfaces;
using System.Collections;
using UnityEngine;

namespace BTF.Resource
{
    public class BerryView : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject grownBerries;
        [SerializeField] private GameObject ungrownBerries;
        [SerializeField] private float regrowTime;

        private BerryController controller;

        public void Bind(BerryController controller)
        {
            this.controller = controller;
            controller.OnBerryCollect += OnCollect;
            controller.OnBerryRegrow += OnRegrow;
        }

        public void Interact()
        {
            if(controller.CanCollect())
            {
                controller.Collect();
            }
        }

        private void OnCollect()
        {
            grownBerries.SetActive(false);
            ungrownBerries.SetActive(true);
            StartCoroutine(RegrowRoutine());
        }

        private IEnumerator RegrowRoutine()
        {
            yield return new WaitForSeconds(regrowTime);
            controller?.Regrow();
        }

        private void OnRegrow()
        {
            grownBerries.SetActive(true);
            ungrownBerries.SetActive(false);
        }
    }
}
