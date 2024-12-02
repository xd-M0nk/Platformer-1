using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")] 
    public float speed = 10;
    public float jumpHeight = 4;
    
    [Header("Ground check")]
    public Transform groundCheck;//character legs
    public float radius = 0.2f;
    public LayerMask groundMask;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    

    private float coyoteBuffer;
    private float jumpBuffer;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
        
        if(isGrounded)
        {
            coyoteBuffer = coyoteTime;
        }
        else
        {
            coyoteBuffer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = jumpBufferTime;
        }
        else
        {
            jumpBuffer -= Time.deltaTime;
        }


        if (jumpBuffer > 0 && coyoteBuffer > 0)
        {
            jumpBuffer = 0;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //apply fall damage
        if(collision.relativeVelocity.y > 25)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
