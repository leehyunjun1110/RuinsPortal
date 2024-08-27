using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(StateMachine stateMachine, GameObject character) : base(stateMachine, character) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        // Idle 애니메이션 플레이
        character.GetComponent<Animator>().Play("Idle");
    }

    public override void UpdateLogic()
    {
        // 이동 입력이 있으면 Run 상태로 전환
        if (Input.GetAxis("Horizontal") != 0)
        {
            stateMachine.ChangeState(new RunState(stateMachine, character));
        }

        // 점프 입력이 있으면 Jump 상태로 전환
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(new JumpState(stateMachine, character));
        }
    }
}