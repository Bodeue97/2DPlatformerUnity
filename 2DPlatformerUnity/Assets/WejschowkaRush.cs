using System.Collections;
using UnityEngine;

public class WejsciowkaRush : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private bool startMovingRight = true;

    private bool moveRight;
    private float originalX;

    void Start()
    {
        originalX = transform.position.x;
        moveRight = startMovingRight;
        StartCoroutine(MoveAndDelay());
    }

    IEnumerator MoveAndDelay()
    {
        if (startMovingRight)
        {
            while (transform.position.x < originalX + moveDistance)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
        else
        {
            while (transform.position.x > originalX - moveDistance)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }

        moveRight = !startMovingRight;

        while (true)
        {
            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

                if (transform.position.x >= originalX + moveDistance)
                {
                    moveRight = false;
                    yield return new WaitForSeconds(Random.Range(0f, 2f));
                }
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

                if (transform.position.x <= originalX - moveDistance)
                {
                    moveRight = true;
                    yield return new WaitForSeconds(Random.Range(0f, 2f));
                }
            }

            yield return null;
        }
    }
}
