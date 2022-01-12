using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodPlatformerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    [HideInInspector]
    public float moveInput;

    private bool isGrounded;
    public Transform bottomRight;
    public Transform topLeft;
    public LayerMask whatIsGround;

    public float hangTime = .2f;
    private float hangCounter;

    public float jumpBufferLength = .1f;
    private float jumpBufferCount;

    public KeyCode mainJumpKey = KeyCode.Space;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       moveInput = Input.GetAxisRaw("Horizontal");

       rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, whatIsGround);

        //switch controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainJumpKey = KeyCode.Space;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mainJumpKey = KeyCode.UpArrow;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mainJumpKey = KeyCode.W;
        }

        //hangtime
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //jump buffer

       if (Input.GetKeyDown(mainJumpKey))
       {
          jumpBufferCount = jumpBufferLength;
       }
       else
       {
           jumpBufferCount -= Time.deltaTime;
       }


        //flipping sprite
        
        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        //jumping
        if (hangCounter > 0f && jumpBufferCount >= 0)
        {
            rb.velocity = Vector2.up * jumpForce;

            jumpBufferCount = 0;
        }

        //reseting the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
