using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class FlappyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravity;
    public Quaternion topRotation;
    public Quaternion midRotation;
    public Quaternion bottomRotation;
    public Transform sprite;
    public TMP_Text pointsText;
    public float maxFallSpeed;
    public float maxJumpSpeed;
    public float jumpForce;
    public float timeBetweenJump;
    private float jumpTimer;
    private bool jumping;
    private int points;
    private float ground;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = timeBetweenJump;
    }

    private void OnEnable()
    {
        ground = (GameManager.Singleton.screenSize.y * -0.5f) - 0.5f;
    }

    private void Update()
    {
        lookDirection();
        checkJumping();
        if (rb.transform.position.y <= ground)
            GameOver();
    }

    private void checkJumping()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer < timeBetweenJump)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && rb.linearVelocityY < maxJumpSpeed)
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

        rb.linearVelocityY = 0f;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumping = false;
    }

    private void doGravity()
    {
        if ((rb.linearVelocityY * -1) < maxFallSpeed) 
        {
            rb.linearVelocityY -= gravity;
        }
    }

    private void lookDirection()
    {
        float currentVelocity = rb.linearVelocityY;
        if (currentVelocity >= 0)
        {
            sprite.rotation = Quaternion.Lerp(midRotation, topRotation, currentVelocity / maxJumpSpeed);
        }
        else
        {
            sprite.rotation = Quaternion.Lerp(midRotation, bottomRotation, (currentVelocity* -1)/ maxFallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        points++;
        pointsText.text = points.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver();

    }

    public void GameOver()
    {
        Debug.Log("GameOVer");
        Time.timeScale = 0f;
    }
}
