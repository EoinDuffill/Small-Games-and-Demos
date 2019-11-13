using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 4f;
    public float airDeceleration = 10f;
    public float jumpSpeed = 4f;

    // Use this for initialization
    void Start () {
		
	}

    bool isIdle;
    bool isLeft;
    bool isJumping = false;
    int isIdleKey = Animator.StringToHash("isIdle");
    int isJumpingKey = Animator.StringToHash("isJumping");
    int isFallingKey = Animator.StringToHash("isFalling");

    // Update is called once per frame
    void Update () {
        Animator a = GetComponent<Animator>();
        a.SetBool(isIdleKey, isIdle);
        a.SetBool(isJumpingKey, isJumping);
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.flipX = isLeft;
    }

    int groundMask = 1 << 8;
    readonly float jumpTimout = 0.25f;
    float jumpTimer = 0;
    int horizontalInput = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        // the new velocity to apply to the character
        Vector2 physicsVelocity = Vector2.zero;
        Rigidbody2D r = GetComponent<Rigidbody2D>();

        isIdle = true;

        // move to the left & right, assiging Idle & left booleans appriopriately 
        horizontalInput = 0;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
            isIdle = false;
            isLeft = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
            isIdle = false;
            isLeft = false;
        }

        if (isJumping)
        {
            isIdle = false;

            float alterHorizontalSpeed = r.velocity.x + (horizontalInput * airDeceleration * Time.fixedDeltaTime);

            if ( Mathf.Abs(alterHorizontalSpeed) > speed)
            {
                physicsVelocity.x = speed * horizontalInput;
            }
            else
            {
                physicsVelocity.x = alterHorizontalSpeed;
            }
        }
        else
        {
            physicsVelocity.x = speed * horizontalInput;
        }

        // this allows the player to jump, but only if not already jumping
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            if (!isJumping && canJump())
            {
                isJumping = true;
                r.velocity = new Vector2(physicsVelocity.x, jumpSpeed);
                jumpTimer = jumpTimout + Time.time;
            }
        }
        // Test the ground immediately below the Player
        // and if it tagged as a Ground layer, then we allow the
        // Player to jump again.
        if(canJump() && isJumping && Time.time > jumpTimer)
        {
            isJumping = false;

        }

        //Assign Velocities
        r.velocity = new Vector2(physicsVelocity.x, r.velocity.y);       

        //Collision with ground check, to ensure no clipping
        FinalCollisionCheck(r, gameObject.GetComponent<CapsuleCollider2D>(), groundMask);
    }

    private bool canJump()
    {
        CapsuleCollider2D playerCollider = gameObject.GetComponent<CapsuleCollider2D>();

        bool bottomLeftRaycast = Physics2D.Raycast(new Vector2 (playerCollider.bounds.min.x, playerCollider.bounds.min.y), -Vector2.up, 0.1f, groundMask);
        bool bottomRightRaycast = Physics2D.Raycast(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y), -Vector2.up, 0.1f, groundMask);

        if(bottomLeftRaycast || bottomRightRaycast)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FinalCollisionCheck(Rigidbody2D rigidBody, CapsuleCollider2D playerCollider, int groundMask)
    {
        // Get the velocity
        Vector2 moveDirection = new Vector2(rigidBody.velocity.x * Time.fixedDeltaTime, 0.2f);

        // Get bounds of Collider
        var bottomRight = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.max.y);
        var topLeft = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);

        // Move collider in direction that we are moving
        bottomRight += moveDirection;
        topLeft += moveDirection;

        // Check if the body's current velocity will result in a collision
        if (Physics2D.OverlapArea(topLeft, bottomRight, groundMask))
        {
            // If so, stop the movement
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            Debug.Log("Collision");
        }
    }
}
