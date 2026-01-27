using BTF.Discovery;
using BTF.Game;
using BTF.Guidance;
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
        [SerializeField] private BerryGuideController berryGuideController;

        private BerryController controller;
        private GameContext context;
        public void Bind(BerryController controller, GameContext context)
        {
            this.controller = controller;
            controller.OnBerryCollect += OnCollect;
            controller.OnBerryRegrow += OnRegrow;
            this.context = context;
        }

        public void Interact()
        {
            if(controller.CanCollect())
            {
                controller.Collect();
            }
            context.Discovery.TryDiscover(DiscoverableItem.Berry);
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
