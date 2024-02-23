using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float distance, scaleValue;
    Vector3 minPos, maxPos;
    int direction = -1;


    void Start()
    {
        maxPos = transform.position + new Vector3(distance, 0, 0);
        minPos = transform.position + new Vector3(-distance, 0, 0);
        scaleValue = transform.localScale.x;
    }

    public void ResetPosition()
    {
        maxPos = transform.position + new Vector3(distance, 0, 0);
        minPos = transform.position + new Vector3(-distance, 0, 0);
    }

    void Update()
    {
        // Simple Roaming Enemy
        if (direction == 1)
        {
            if (transform.position.x < maxPos.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, maxPos, speed * Time.deltaTime);
            }
            else
            {
                direction = -1;
                transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                // spriteRenderer.flipX = false;
            }
        }
        else
        {

            if (transform.position.x > minPos.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, minPos, speed * Time.deltaTime);
            }
            else
            {
                direction = 1;
                transform.localScale = new Vector3(-scaleValue, scaleValue, scaleValue);
                // spriteRenderer.flipX = true;
            }
        }
    }

}
