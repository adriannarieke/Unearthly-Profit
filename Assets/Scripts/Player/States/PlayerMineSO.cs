using UnityEngine;

[CreateAssetMenu(fileName = "Player-Mine", menuName = "Player States/Mine")]
public class PlayerMineSO : StateSOBase<Player>
{
    [SerializeField] Timer miningCooldown = new Timer(0.5f);
    public Crystal targetCrystal;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        targetCrystal = null;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        miningCooldown.Update(Time.deltaTime);
        if (miningCooldown.Laps > 0)
        {
            targetCrystal.Mine(out bool fullyMined);
            if (fullyMined)
            {
                entity.StateMachine.ChangeState(entity.movement == Vector2.zero ? entity.IdleState : entity.MoveState);
            }
            else
            {
                miningCooldown.Reset();
            }
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(Player entity)
    {
        base.Initialize(entity);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        miningCooldown.Reset();
    }
}
