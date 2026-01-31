using UnityEngine;

public class State<TEntity> where TEntity : MonoBehaviour
{
    protected TEntity entity;

    protected StateSOBase<TEntity> stateSO;

    public State(TEntity entity, StateSOBase<TEntity> stateSO)
    {
        this.entity = entity;
        this.stateSO = stateSO;
        stateSO.Initialize(entity);
    }
    
    public void EnterState()
    {
        stateSO.DoEnterLogic();
    }
    public void ExitState()
    {
        stateSO.DoExitLogic();
    }
    public void FrameUpdate()
    {
        stateSO.DoFrameUpdateLogic();
    }
    public void PhysicsUpdate()
    {
        stateSO.DoPhysicsLogic();
    }
}
