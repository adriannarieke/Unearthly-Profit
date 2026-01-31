using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player-Refill", menuName = "Player States/Refill")]
public class PlayerRefillSO : StateSOBase<Player>
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        MainScreen.oxygenMeterIsActive = false;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        MainScreen.oxygenMeterIsActive = true;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        MainScreen.AddOxygen(entity.MaskStation.OxygenRefillSpeed * Time.deltaTime);
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
    }
}
