using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

public class PlayerAttackState : IState<PlayerController>
{
    private PlayerController owner;

    public void SetOwner(PlayerController owner) => this.owner = owner;

    public void OnStateEnter()
    {
        owner.IsAttacking = true;
        owner.LockMovement(); 
        owner.StopMovement();
        owner.View.PlayAttackAnimation();
    }

    public void OnStateExit()
    {
        owner.IsAttacking = false;
        owner.UnlockMovement(); 
    }

    public void Update()
    {
        Vector2 input = owner.GetMoveInput();

        if (input.sqrMagnitude > 0.001f)
        {
            owner.View.UpdateFacingDirection(input);
        }
    }
}

