namespace BTF.Resource
{
    public class TreeModel
    {
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }

        public bool IsCut { get; private set; }

        public TreeModel(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
            IsCut = false;
        }

        public void ReduceHP(int damage)
        {
            if (IsCut) return;

            CurrentHP -= damage;

            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                IsCut = true;
            }
        }

        public void Reset()
        {
            CurrentHP = MaxHP;
            IsCut = false;
        }
    }
}
