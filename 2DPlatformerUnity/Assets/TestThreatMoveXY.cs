using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThreatMoveSquare : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float xMoveDistance = 10f;
    [SerializeField] private float yMoveDistance = 10f;
    [SerializeField] private bool inverseMovement = false;
    [SerializeField] private bool reverseMovement = false;

    private bool moveRight = true;
    private bool moveUp = false;
    private bool moveLeft = false;
    private bool moveDown = false;

    private float originalX;
    private float originalY;

    void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;
    }

    void Update()
    {
        if (moveRight)
        {
            Move(Vector2.right, xMoveDistance);
            if (Mathf.Abs(transform.position.x - originalX) >= xMoveDistance)
            {
                moveRight = false;
                moveUp = true;
            }
        }
        else if (moveUp)
        {
            Move(Vector2.up, yMoveDistance);
            if (Mathf.Abs(transform.position.y - originalY) >= yMoveDistance)
            {
                moveUp = false;
                moveLeft = true;
            }
        }
        else if (moveLeft)
        {
            Move(Vector2.left, xMoveDistance);
            if (Mathf.Abs(transform.position.x - originalX) <= 0.1f)
            {
                moveLeft = false;
                moveDown = true;
            }
        }
        else if (moveDown)
        {
            Move(Vector2.down, yMoveDistance);
            if (Mathf.Abs(transform.position.y - originalY) <= 0.1f)
            {
                moveDown = false;
                moveRight = true;
            }
        }
    }

    void Move(Vector2 direction, float distance)
    {
        float movement = moveSpeed * Time.deltaTime;
        
        if (inverseMovement)
            movement *= -1; // Inverts the movement

        if (reverseMovement)
        {
            movement *= -1; // Reverses the movement without inversion
            if (direction == Vector2.right)
                direction = Vector2.left;
            else if (direction == Vector2.left)
                direction = Vector2.right;
            else if (direction == Vector2.up)
                direction = Vector2.down;
            else if (direction == Vector2.down)
                direction = Vector2.up;
        }

        transform.Translate(direction * movement);
    }
}
