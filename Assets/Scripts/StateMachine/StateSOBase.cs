using UnityEngine;

public class StateSOBase<TEntity> : ScriptableObject where TEntity : MonoBehaviour
{
    protected TEntity entity;
    protected GameObject gameObject;
    protected Transform transform;

    public virtual void Initialize(TEntity entity)
    {
        this.entity = entity;
        gameObject = entity.gameObject;
        transform = entity.transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { }
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsLogic() { }
    public virtual void ResetValues() { }
}
