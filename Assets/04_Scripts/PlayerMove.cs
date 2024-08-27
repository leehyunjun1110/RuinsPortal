using UnityEngine;

public abstract class BaseState
{
    protected StateMachine stateMachine;
    protected GameObject character;

    public BaseState(StateMachine stateMachine, GameObject character)
    {
        this.stateMachine = stateMachine;
        this.character = character;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;

    public void Initialize(BaseState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        currentState.UpdatePhysics();
    }
}
