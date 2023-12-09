using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThreatMoveY : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxMoveUpDistance = 10f;
    [SerializeField] private float maxMoveDownDistance = 10f;
    [SerializeField] private bool moveUpFirst = true; 
    private bool moveUp;
    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
        moveUp = moveUpFirst;
    }

    void Update()
    {
        if (moveUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

            if (transform.position.y >= originalY + maxMoveUpDistance)
            {
                moveUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

            if (transform.position.y <= originalY - maxMoveDownDistance)
            {
                moveUp = true;
            }
        }
    }
}
