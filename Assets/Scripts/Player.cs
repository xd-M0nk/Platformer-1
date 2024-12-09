using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Health health;

    [Header("Movement")] 
    public float speed = 10;
    public float jumpHeight = 4;
    public float dashSpeed = 20;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    
    [Header("Ground check")]
    public Transform groundCheck;//character legs
    public float radius = 0.2f;
    public LayerMask groundMask;


    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public int maxJumps = 2;

    [Header("Sound")]
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioSource walkSoundSource;

    private AudioSource audioSource;
    private int remainingJumps;
    private float coyoteBuffer;
    private float jumpBuffer;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
        
        if(isGrounded)
        {
            coyoteBuffer = coyoteTime;
            remainingJumps = maxJumps;
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


        if (jumpBuffer > 0 && (coyoteBuffer > 0 || remainingJumps > 0))
        {
            audioSource.PlayOneShot(jumpSound);
            jumpBuffer = 0;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if(!isGrounded)
            {
                remainingJumps--;
            }
        }

        //dashing
        if(Input.GetButtonDown("Dash") && dashCooldownTime <= 0)
        {
            audioSource.PlayOneShot(dashSound);
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTime = dashCooldown;        
        }

        if(isDashing)
        {
            if(dashTime > 0)
            {
                rb.velocity = new Vector2(dashSpeed * horizontal, rb.velocity.y);
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
        dashCooldownTime -= Time.deltaTime;

        if(isGrounded && rb.velocity.magnitude > 0)
        {
            if(!walkSoundSource.isPlaying)
            {
                walkSoundSource.Play();
            }
        }
        else
        {
            walkSoundSource.Stop();
        }
    }

    void FixedUpdate()
    {
        if(!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
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
            health.TakeDamage(1);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
