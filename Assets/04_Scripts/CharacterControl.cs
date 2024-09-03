using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private bool move = false;

    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Parallax parallax;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public List<GameObject> objs = new List<GameObject>();
    private bool isGrounded;

    private bool facingRight = true;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        float moveInput = Input.GetAxis("Horizontal");

        HandleMovement(moveInput);
        HandleJumping();

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
                parallax.MoveRight();
                if (!facingRight)
                {
                    Flip();
                }
            }
            else if (moveInput < 0)
            {
                parallax.MoveLeft();
                if (facingRight)
                {
                    Flip();
                }
            }
        }
    }

    void HandleMovement(float moveInput)
    {
        move = true;
        if (Mathf.Abs(moveInput) > 0.1f && isGrounded)
        {
            animator.SetBool("isRunning", true);
            parallax.MoveUp();
        }
        else
        {
            animator.SetBool("isRunning", false);
            parallax.MoveDown();
        }
    }

void HandleJumping()
{
    if (isGrounded)
    {
        isJumping = false; // 지면에 닿았을 때 점프 상태를 초기화
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
        for(int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if(objs[ix] == other.gameObject)
            {
                none = false;
            }
        }
        if(none == true && other.gameObject.GetComponent<Rigidbody2D>() != null && other.gameObject.GetComponent<Rigidbody2D>().gravityScale != 0 && other.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            if(other.transform.position.y - other.transform.localScale.y * other.gameObject.GetComponent<BoxCollider2D>().size.y / 2 + 0.05f >= transform.position.y + transform.localScale.y * GetComponent<BoxCollider2D>().size.y / 2)
            {
                objs.Add(other.gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        for(int ix = objs.Count - 1; ix >= 0; ix--)
        {
            if(objs[ix] == other.gameObject)
            {
                objs.RemoveAt(ix);
            }
        }
    }
}