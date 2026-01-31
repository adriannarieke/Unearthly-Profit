using UnityEngine;

[CreateAssetMenu(fileName = "Player-Idle", menuName = "Player States/Idle")]
public class PlayerIdleSO : StateSOBase<Player>
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        if (entity.movement != Vector2.zero)
        {
            entity.StateMachine.ChangeState(entity.MoveState);
            return;
        }
        base.DoFrameUpdateLogic();
        entity.SetMovement();
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
