using UnityEngine;

public class JumpState : BaseState
{
    private Rigidbody2D rb;
    private float jumpForce = 7f;

    public JumpState(StateMachine stateMachine, GameObject character) : base(stateMachine, character) 
    {
        rb = character.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        Debug.Log("Entering Jump State");
        // Jump 애니메이션 플레이
        character.GetComponent<Animator>().Play("Jump");
        // 캐릭터 점프
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public override void UpdateLogic()
    {
        // 캐릭터가 땅에 닿으면 Idle 또는 Run 상태로 전환
        if (character.GetComponent<CharacterController2D>().IsGrounded())
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                stateMachine.ChangeState(new RunState(stateMachine, character));
            }
            else
            {
                stateMachine.ChangeState(new IdleState(stateMachine, character));
            }
        }
    }
}