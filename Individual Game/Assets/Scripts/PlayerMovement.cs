using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] public int extraJumpsValue;
    private int extraJumps;
    private bool isFacingRight = true;

    [SerializeField] private bool dashAvail;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashTime = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private float dashCooldown = 0.2f;

    private enum MovementState { idle, running, jumping, falling };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        extraJumps = extraJumpsValue;
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        UpdateAnimationState();

        if (IsGrounded())
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAvail && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        Vector3 localScale = transform.localScale;

        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
