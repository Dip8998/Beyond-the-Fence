namespace BTF.UI.Health
{
    public sealed class PlayerHealthUIModel
    {
        public int CurrentHP { get; private set; }
        public int MaxHP { get; private set; }

        public float Normalized => MaxHP == 0 ? 0f : (float)CurrentHP / MaxHP;

        public void Set(int current, int max)
        {
            CurrentHP = current;
            MaxHP = max;
        }
    }
}
