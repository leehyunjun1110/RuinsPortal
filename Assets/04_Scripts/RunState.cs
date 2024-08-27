using UnityEngine;

public class RunState : BaseState
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;

    public RunState(StateMachine stateMachine, GameObject character) : base(stateMachine, character) 
    {
        rb = character.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        Debug.Log("Entering Run State");
        // Run 애니메이션 플레이
        character.GetComponent<Animator>().Play("Run");
    }

    public override void UpdateLogic()
    {
        // 이동 입력이 없으면 Idle 상태로 전환
        if (Input.GetAxis("Horizontal") == 0)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, character));
        }

        // 점프 입력이 있으면 Jump 상태로 전환
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(new JumpState(stateMachine, character));
        }
    }

    public override void UpdatePhysics()
    {
        // 캐릭터 이동
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }
}