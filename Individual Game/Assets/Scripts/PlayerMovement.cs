using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private bool dashAvail;
    [SerializeField] private float dashSpeed = 14f;
    [SerializeField] private float dashTime = 0.5f;
    private Vector2 dashDir;
    private bool isDashing;
    private bool canDash = true;

    private enum MovementState { idle, running, jumping, falling };

    [SerializeField] private AudioSource jumpSoundEffect;

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

        if (dashAvail)
        {
            Dashing();
        }
    }

    private void Dashing()
    {
        
         if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
         {
            isDashing = true;
            canDash = false;
            dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashDir == Vector2.zero)
            {
                dashDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing());
         }
        

        if (isDashing)
        {
            rb.velocity = dashDir.normalized * dashSpeed;
            return;
        }

        if (IsGrounded())
        {
            canDash = true;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
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
