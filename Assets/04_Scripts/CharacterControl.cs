using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialize(new IdleState(stateMachine, gameObject));
    }

    public bool IsGrounded()
    {
        // 땅에 닿아있는지 여부를 체크하는 로직
        // 예를 들어, Raycast를 사용하여 체크할 수 있음
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
    }
}