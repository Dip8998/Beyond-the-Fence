using BTF.Interfaces;
using BTF.Player;
using UnityEngine;

public class PlayerAttackState : IState<PlayerController>
{
    private PlayerController owner;
    private float timer;

    private const float MAX_ATTACK_TIME = 1f; 

    public void SetOwner(PlayerController owner) => this.owner = owner;

    public void OnStateEnter()
    {
        timer = 0f;
        owner.IsAttacking = true;

        owner.LockMovement();
        owner.StopMovement();
        owner.View.PlayAttackAnimation();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        Vector2 input = owner.GetMoveInput();
        if (input.sqrMagnitude > 0.001f)
        {
            owner.View.UpdateFacingDirection(input);
        }

        if (timer >= MAX_ATTACK_TIME)
        {
            owner.ChangeState(PlayerStates.Idle);
        }
    }

    public void OnStateExit()
    {
        owner.IsAttacking = false;
        owner.UnlockMovement();
    }
}