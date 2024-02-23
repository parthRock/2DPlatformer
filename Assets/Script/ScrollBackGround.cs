using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{

    public float scrollSpeed = -5f;
    Vector2 startPos;
    public float maxValue = 5;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * scrollSpeed, 10 * Time.deltaTime);


        if (transform.position.x < maxValue)
        {

            {
                transform.position = startPos;
            }
        }
    }
}
