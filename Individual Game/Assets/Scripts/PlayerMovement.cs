using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem dust;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private bool isFacingRight = true;

    // when extraJumpsValue is above 1, we can double jump
    [SerializeField] public int extraJumpsValue;
    private int extraJumps;

    // variables for dashing
    [SerializeField] private bool dashAvail;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashTime = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private bool dashReset = true;
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
        if (isDashing) // if we are dashing, we don't want to override it with other movement code
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        UpdateAnimationState();

        if (IsGrounded())
        {
            extraJumps = extraJumpsValue; // resets the double jump when player hits the ground
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonDown("Jump") && extraJumps > 0) // allows the player to jump however many extra times we set it
        {
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        }

        if (dashReset && IsGrounded())
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAvail && canDash)
        {
            CreateDust();
            StartCoroutine(Dash());
        }

        Flip();
    }

    /*
     * flips the player sprite according to which direction we should be facing
     * using the localScale to make the default dash direction work correctly
     */
    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            CreateDust();
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashReset = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // makes the dash in a straight line, rather than falling
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        
        // gives a cooldown before resetting to the usual gravity
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        // cooldown until we can dash again
        yield return new WaitForSeconds(dashCooldown);
        dashReset = true;
    }

    /*
     * uses the MovementState enum to update player animations
     */
    private void UpdateAnimationState()
    {
        MovementState state;

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

    void CreateDust()
    {
        dust.Play();
    }

    /*
     * ground check
     */
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
