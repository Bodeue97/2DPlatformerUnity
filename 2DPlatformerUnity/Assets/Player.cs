using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private float moveX;
    [SerializeField] private float movSpeed = 5f, jumpSpeed = 10f;
    [SerializeField] private Text ECTScount;
    [SerializeField] private Text SpeedCount;
    [SerializeField] private Text JumpBoost;

    private Coroutine speedCoroutine;
     private Coroutine jumpCoroutine;
    private int jumpCount = 2;
    private bool isGrounded;
    private bool isJumping;
    private bool isDoubleJumping;
    private int ects = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isGrounded = true;
        isJumping = false;
        isDoubleJumping = false;
        UpdateSpeedUI();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * movSpeed, rb.velocity.y);

        if (isGrounded)
        {
            jumpCount = 2;
            isJumping = false;
            isDoubleJumping = false;
        }

        if (jumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isGrounded = false;
            jumpCount--;

            isJumping = jumpCount > 0;
            isDoubleJumping = !isJumping && jumpCount == 0;
        }

        UpdateAnimation();
        UpdateSpeedUI();
        UpdateJumpUI();
    }


    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rektorskie"))
        {
            Destroy(collision.gameObject);
            movSpeed += 5f;

            if (speedCoroutine != null)
                StopCoroutine(speedCoroutine);

            speedCoroutine = StartCoroutine(ReduceSpeedAfterDelay(5f));

            UpdateSpeedUI();
            UpdateJumpUI();
        }
        
        if (collision.gameObject.CompareTag("Zdalne"))
        {
            Destroy(collision.gameObject);
            jumpSpeed += 5f;

            if (jumpCoroutine != null)
                StopCoroutine(jumpCoroutine);

            jumpCoroutine = StartCoroutine(ReduceJumpAfterDelay(5f));

            UpdateSpeedUI();
            UpdateJumpUI();
        }

        if (collision.gameObject.CompareTag("ECTS"))
        {
            Destroy(collision.gameObject);
            ects++;
            ECTScount.text = "ECTS: " + ects;
        }
    }
    private void UpdateSpeedUI()
    {
        
        SpeedCount.text = "PRĘDKOŚĆ: " + movSpeed;
    }
    private void UpdateJumpUI()
    {
        if(jumpSpeed > 11f)
        JumpBoost.text = "LEPSZY SKOK!!";
        if(jumpSpeed < 11f)
        JumpBoost.text = "";
    }
    private IEnumerator ReduceSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        movSpeed -= 5f;
    }
    private IEnumerator ReduceJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        jumpSpeed -= 5f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(moveX) > 0 && isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDoubleJumping", isDoubleJumping);

        sr.flipX = moveX < 0;
    }
}
