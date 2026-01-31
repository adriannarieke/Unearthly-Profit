using UnityEngine;

public class StateMachine<TEntity> where TEntity : MonoBehaviour
{
    public State<TEntity> CurrentState { get; private set; }

    public void Initialize(State<TEntity> startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(State<TEntity> newState)
    {
        if (newState != null)
        {
            CurrentState.ExitState();
            newState.EnterState();
            CurrentState = newState;
        }
    }
}
