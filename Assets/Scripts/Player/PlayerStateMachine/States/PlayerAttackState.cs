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
        owner.StopMovement();
        owner.View.PlayAttackAnimation();
    }

    public void OnStateExit()
    {
        owner.IsAttacking = false;
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
