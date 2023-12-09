using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private float moveX;
    [SerializeField] private float movSpeed = 5f, jumpSpeed = 10f;
    [SerializeField] private Text ECTScount, SpeedCount, JumpBoost;
    [SerializeField] private AudioSource jumpSound, collectSound, zaliczenieSound;
    [SerializeField] private AudioSource speedPowerUpSound, speedPowerEndSound;
    [SerializeField] private AudioSource jumpPowerUpSound, jumpPowerEndSound;
    private int maxEcts = 4;
    private Coroutine speedCoroutine, jumpCoroutine;
    private int jumpCount = 2;
    private bool isGrounded, isJumping, isDoubleJumping;
    private int ects = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isGrounded = true;
        isJumping = false;
        isDoubleJumping = false;
        UpdateUI();
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
            HandleJump();
        }

        UpdatePlayerState();
        UpdateUI();
    }

    private void HandleJump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        isGrounded = false;
        jumpCount--;
        isJumping = jumpCount > 0;
        isDoubleJumping = !isJumping && jumpCount == 0;
    }

    private void UpdatePlayerState()
    {
        bool isRunning = Mathf.Abs(moveX) > 0 && isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDoubleJumping", isDoubleJumping);
        sr.flipX = moveX < 0;
    }

    private void UpdateUI()
    {
        SpeedCount.text = "PRĘDKOŚĆ: " + movSpeed;
        JumpBoost.text = jumpSpeed > 11f ? "LEPSZY SKOK!!" : "";
        ECTScount.text = "ECTS: " + ects + "/" + maxEcts;
    }

    private IEnumerator ReduceSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        movSpeed -= 5f;
        speedPowerEndSound.Play();
    }

    private IEnumerator ReduceJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        jumpSpeed -= 5f;
        jumpPowerEndSound.Play();
    }

    private IEnumerator LoadSceneAfterSound(string sceneName, float delay)
    {
        zaliczenieSound.Play();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rektorskie"))
        {
            speedPowerUpSound.Play();
            Destroy(collision.gameObject);
            movSpeed += 5f;
            if (speedCoroutine != null)
                StopCoroutine(speedCoroutine);
            speedCoroutine = StartCoroutine(ReduceSpeedAfterDelay(7f));
        }

        if (collision.CompareTag("Zdalne"))
        {
            jumpPowerUpSound.Play();
            Destroy(collision.gameObject);
            jumpSpeed += 5f;
            if (jumpCoroutine != null)
                StopCoroutine(jumpCoroutine);
            jumpCoroutine = StartCoroutine(ReduceJumpAfterDelay(7f));
        }

        if (collision.CompareTag("ECTS"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            ects++;
        }

        if (collision.CompareTag("Zaliczenie1") && ects == maxEcts)
        {

            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(LoadSceneAfterSound("BazyDanychLevel2", 3f));
            ects = 0;
        }

        if (collision.CompareTag("Zaliczenie2") && ects == maxEcts)
        {
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(LoadSceneAfterSound("AnalizaMatematycznaLevel3", 3f));
            ects = 0;
        }
         if (collision.CompareTag("Zaliczenie") && ects == 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(LoadSceneAfterSound("KoniecScene", 3f));
            ects = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
            isGrounded = true;
    }
}
