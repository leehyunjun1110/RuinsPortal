using UnityEngine;
using System.Collections.Generic;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private bool move = false;
    private bool jump = false;

    private Animator animator;
    private Rigidbody2D rb;

    private ParallaxMovement parallaxMovement;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public List<GameObject> objs = new List<GameObject>();
    private bool isGrounded;

    private bool facingRight = true;
    private bool isJumping = false;
    private float previousY;

    private bool jumpactivate = false;

    void Start()
    {
        previousY = transform.position.y;
        isJumping = false;
        isGrounded = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        var parallaxManager = FindObjectOfType<ParallaxManager>();
        parallaxMovement = new ParallaxMovement(parallaxManager, 0.1f, 20f); // 초기값은 필요에 따라 변경
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        float moveInput = Input.GetAxis("Horizontal");

        HandleMovement(moveInput);
        HandleJumping();
        IsJUMP();

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (move != true)
        {
            for (int ix = objs.Count - 1; ix >= 0; ix--)
            {
                objs[ix].GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            }
        }

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            if (moveInput > 0)
            {
                parallaxMovement.MoveRight();
                if (!facingRight)
                {
                    Flip();
                }
            }
            else if (moveInput < 0)
            {
                parallaxMovement.MoveLeft();
                if (facingRight)
                {
                    Flip();
                }
            }
        }
        else if (Mathf.Abs(moveInput) == 0)
        {
            parallaxMovement.Stop();
        }
    }

    void HandleMovement(float moveInput)
    {
        move = true;
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void IsJUMP()
    {
        float currentY = transform.position.y;
        float jumpSpeed = Mathf.Abs(rb.velocity.y);

        if (currentY > previousY)
        {
            if (!isJumping)
            {
                Debug.Log("점프 시작");
                isJumping = true;
                parallaxMovement.MoveUp(jumpSpeed);
            }
            else
            {
                Debug.Log("점프중입니다");
                parallaxMovement.MoveUp(jumpSpeed);
            }
        }
        else if (currentY < previousY)
        {
            if (isJumping)
            {
                Debug.Log("하강중입니다");
                parallaxMovement.MoveDown(jumpSpeed);
            }
        }
        else if (currentY == previousY && isGrounded)
        {
            if (isJumping)
            {
                Debug.Log("점프 종료");
                isJumping = false;
                parallaxMovement.Stop();
            }
            else
            {
                Debug.Log("정지 상태 유지");
            }
        }

        previousY = currentY;
    }

    void HandleJumping()
    {
        if (isGrounded)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        bool none = true;
        for (int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if (objs[ix] == other.gameObject)
            {
                none = false;
            }
        }
        if (none && other.gameObject.GetComponent<Rigidbody2D>() != null && other.gameObject.GetComponent<Rigidbody2D>().gravityScale != 0 && other.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            if (other.transform.position.y - other.transform.localScale.y * other.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.05f >= transform.position.y + transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2)
            {
                objs.Add(other.gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        for (int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if (objs[ix] == other.gameObject)
            {
                objs.RemoveAt(ix);
            }
        }
    }
}