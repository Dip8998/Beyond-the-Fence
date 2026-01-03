using System;

namespace BTF.Resource
{
    public sealed class TreeController
    {
        private readonly TreeModel model;

        public event Action OnTreeCut;
        public event Action OnTreeRegrow;

        public TreeController(TreeModel model)
        {
            this.model = model;
        }

        public void TakeDamage(int damage)
        {
            model?.ReduceHP(damage);

            if(model.CurrentHP <= 0)
            {
                OnTreeCut?.Invoke();
            }
        }

        public void Regrow()
        {
            model?.Reset();
            OnTreeRegrow?.Invoke();
        }
    }
}
