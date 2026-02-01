using UnityEngine;

[CreateAssetMenu(fileName = "Player-Move", menuName = "Player States/Move")]
public class PlayerMoveSO : StateSOBase<Player>
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.Play("PlayerWalk");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        if (entity.movement == Vector2.zero)
        {
            entity.StateMachine.ChangeState(entity.IdleState);
            return;
        }
        base.DoFrameUpdateLogic();
        entity.SetMovement();
        if (entity.movement.x != 0f)
        {
            entity.SR.flipX = entity.movement.x > 0f;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        entity.Move();
    }

    public override void Initialize(Player entity)
    {
        base.Initialize(entity);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
