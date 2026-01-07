using UnityEngine;

namespace BTF.Player
{
    public sealed class PlayerModel
    {
        public int CurrentHP;
        public int MaxHP;
        public float MoveSpeed;
        public Vector2 Velocity;

        public bool IsInvincible;
        public float InvincibleTimer;
        public bool HasFenceKey { get; private set; }
        public bool HasWeapon { get; private set; }

        public PlayerModel(int maxHP, float moveSpeed)
        {
            MaxHP = maxHP;
            MoveSpeed = moveSpeed;
            CurrentHP = MaxHP;
        }

        public void GiveFenceKey()
        {
            HasFenceKey = true;
        }

        public void GiveWeapon()
        {
            HasWeapon = true;
        }
    }
}
