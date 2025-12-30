using UnityEngine;

namespace BTF.Player
{
	public sealed class PlayerModel
	{
		public int CurrentHP;
		public int MaxHP;
		public float MoveSpeed;
		public Vector2 Velocity;

		public PlayerModel(int maxHP, float moveSpeed)
		{
			MaxHP = maxHP;
			MoveSpeed = moveSpeed;
			CurrentHP = MaxHP;
		}
	}
}
