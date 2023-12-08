using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Threat"))
        {
            animator.SetTrigger("death");
            rb.bodyType = RigidbodyType2D.Static;

        }

    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
