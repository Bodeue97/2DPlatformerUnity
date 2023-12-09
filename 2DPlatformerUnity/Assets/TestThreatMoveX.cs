using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThreatMoveX : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxMoveRightDistance = 10f;
    [SerializeField] private float maxMoveLeftDistance = 10f;
    [SerializeField] private bool moveRightFirst = true; 

    private bool moveRight;
    private float originalX;

    void Start()
    {
        originalX = transform.position.x;
        moveRight = moveRightFirst;
    }

    void Update()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= originalX + maxMoveRightDistance)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x <= originalX - maxMoveLeftDistance)
            {
                moveRight = true;
            }
        }
    }
}
