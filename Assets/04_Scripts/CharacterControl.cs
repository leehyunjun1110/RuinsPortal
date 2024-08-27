using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private IState currentState;
    private Vector3 lastDirection;

    void Start()
    {
        // 초기 상태를 IdleState로 설정
        ChangeState(new IdleState(gameObject));
        lastDirection = transform.localScale; // 현재 스케일 저장 (캐릭터의 방향)
    }

    void Update()
    {
        // 현재 상태의 Update 로직 실행
        currentState?.Update();

        // 키 입력에 따라 방향 전환
        HandleInput();

        Debug.DrawRay(transform.position, Vector2.down);
        if (IsGrounded() == true){
            Debug.Log("왜안돼지");    
        }
    }

    void FixedUpdate()
    {
        // 현재 상태의 물리 로직 실행
        currentState?.FixedUpdate();
    }

    public void ChangeState(IState newState)
    {
        // 기존 상태를 종료하고 새로운 상태로 전환
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsGrounded()
    {
        // 땅에 닿아있는지 확인하는 로직 (예: Raycast)
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            // 키 입력에 따라 캐릭터의 방향을 바꿉니다.
            Vector3 scale = transform.localScale;

            if (horizontalInput > 0) // 오른쪽으로 움직일 때
            {
                scale.x = Mathf.Abs(scale.x); // 오른쪽 방향
            }
            else if (horizontalInput < 0) // 왼쪽으로 움직일 때
            {
                scale.x = -Mathf.Abs(scale.x); // 왼쪽 방향
            }

            transform.localScale = scale; // 변경된 스케일을 적용하여 방향 전환
            lastDirection = scale; // 방향을 기억해둠
        }
    }
}