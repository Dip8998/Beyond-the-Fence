namespace BTF.Resource
{
	public class TreeModel 
	{
		public int MaxHP { get; }
		public int CurrentHP {  get; private set; }

		public TreeModel(int maxHP)
		{
			MaxHP = maxHP;
			CurrentHP = maxHP;
		}

		public void ReduceHP(int damage) => CurrentHP -=damage;

		public void Reset() => CurrentHP = MaxHP;
	}
}
