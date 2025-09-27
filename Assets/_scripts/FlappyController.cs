using UnityEngine;

public class FlappyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravity;
    public Quaternion topRotation;
    public Quaternion midRotation;
    public Quaternion bottomRotation;
    public float maxFallSpeed;
    public float maxJumpSpeed;
    public float jumpForce;
    public float timeBetweenJump;
    private float jumpTimer;

    private bool jumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = timeBetweenJump;
    }

    private void Update()
    {
        lookDirection();
        checkJumping();
    }

    private void checkJumping()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer < timeBetweenJump)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jumping = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        doGravity();
        doJump();
    }

    private void doJump()
    {
        if (!jumping)
            return;

        rb.AddForce(new Vector2(0, jumpForce));
        jumping = false;
    }

    private void doGravity()
    {
        if ((rb.linearVelocityY * -1) < maxFallSpeed) 
        {
            rb.AddForce(new Vector2(0, -gravity));
        }
    }

    private void lookDirection()
    {
        float currentVelocity = rb.linearVelocityY;
        if (currentVelocity >= 0)
        {
            Quaternion.Lerp(midRotation, topRotation, currentVelocity / maxJumpSpeed);
        }
        else
        {
            Quaternion.Lerp(midRotation, bottomRotation, (currentVelocity* -1)/ maxFallSpeed);
        }
    }
}
