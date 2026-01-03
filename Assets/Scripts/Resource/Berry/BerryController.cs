using System;

namespace BTF.Resource
{
    public sealed class BerryController
    {
        private readonly BerryModel model;

        public event Action OnBerryCollect;
        public event Action OnBerryRegrow;

        public BerryController(BerryModel model)
        {
            this.model = model;
        }

        public bool CanCollect() => model.IsAvailable;

        public void Collect()
        {
            if (!model.IsAvailable) return;

            model?.Consume();
            OnBerryCollect.Invoke();
        }

        public void Regrow()
        {
            model?.Regrow();
            OnBerryRegrow.Invoke();
        }
    }
}
