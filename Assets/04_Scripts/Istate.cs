using UnityEngine;

public interface IState
{
    void Enter();   // 상태에 진입할 때 호출
    void Update();  // 상태 동안 매 프레임 호출
    void FixedUpdate(); // 물리 업데이트가 필요할 때 호출
    void Exit();    // 상태를 떠날 때 호출
}

public class IdleState : IState
{
    private GameObject character;
    private Animator animator;

    public IdleState(GameObject character)
    {
        this.character = character;
        animator = character.GetComponent<Animator>();
    }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
        animator.SetBool("isIdle", true);
    }

    public void Update()
    {
        // 이동 입력이 있으면 Run 상태로 전환
        if (Input.GetAxis("Horizontal") != 0)
        {
            character.GetComponent<CharacterControl>().ChangeState(new RunState(character));
        }

        // 점프 입력이 있고, 캐릭터가 땅에 닿아있을 때만 Jump 상태로 전환
        if (Input.GetButtonDown("Jump") && character.GetComponent<CharacterControl>().IsGrounded())
        {
            animator.SetBool("isIdle", false);
            character.GetComponent<CharacterControl>().ChangeState(new JumpState(character));
        }
    }

    public void FixedUpdate() { }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
        animator.SetBool("isIdle", false);
    }
}

public class RunState : IState
{
    private GameObject character;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveSpeed = 5f;

    public RunState(GameObject character)
    {
        this.character = character;
        rb = character.GetComponent<Rigidbody2D>();
        animator = character.GetComponent<Animator>();
    }

    public void Enter()
    {
        Debug.Log("Entering Run State");
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", true);
    }

    public void Update()
    {
        // 이동 입력이 없으면 Idle 상태로 전환
        if (Input.GetAxis("Horizontal") == 0)
        {
            character.GetComponent<CharacterControl>().ChangeState(new IdleState(character));
        }

        // 점프 입력이 있고, 캐릭터가 땅에 닿아있을 때만 Jump 상태로 전환
        if (Input.GetButtonDown("Jump") && character.GetComponent<CharacterControl>().IsGrounded())
        {
            animator.SetBool("isRunning", false);
            character.GetComponent<CharacterControl>().ChangeState(new JumpState(character));
        }
    }

    public void FixedUpdate()
    {
        // 캐릭터 이동
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    public void Exit()
    {
        Debug.Log("Exiting Run State");
        animator.SetBool("isRunning", false);
    }
}

public class JumpState : IState
{
    private GameObject character;
    private Rigidbody2D rb;
    private Animator animator;
    private float jumpForce = 7f;

    public JumpState(GameObject character)
    {
        this.character = character;
        rb = character.GetComponent<Rigidbody2D>();
        animator = character.GetComponent<Animator>();
    }

    public void Enter()
    {
        Debug.Log("Entering Jump State");
        animator.SetTrigger("Jump");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Update()
    {
        // 캐릭터가 땅에 닿으면 Idle 또는 Run 상태로 전환
        if (character.GetComponent<CharacterControl>().IsGrounded())
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                character.GetComponent<CharacterControl>().ChangeState(new RunState(character));
            }
            else
            {
                character.GetComponent<CharacterControl>().ChangeState(new IdleState(character));
            }
        }
    }

    public void FixedUpdate()
    {
        // 추가적인 물리 업데이트가 필요할 경우 여기에 추가 가능
    }

    public void Exit()
    {
        Debug.Log("Exiting Jump State");
        animator.ResetTrigger("Jump");
    }
}