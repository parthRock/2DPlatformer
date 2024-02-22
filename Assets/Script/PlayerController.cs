using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, speedUp, jumpSpeed, wallCheckDistance,health;
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public bool iswallDetect;
    public Transform wallCheckRayPoint;
    public LayerMask wallDetectLayer;
    float dashCoolDownTimer;
    Rigidbody2D rb;
    int jumpsRemaining = 2;
    bool facingRight = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // Player Movement
        if (!iswallDetect)
        {
            Vector3 _speed = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime) * speedUp;
            transform.Translate(_speed);


            if (_speed.x < 0 && facingRight) // Player Flip
            {
                Flip();
            }
            else if (_speed.x > 0 && !facingRight)
            {
                Flip();
            }
        }
        else
        {
            speed = 0;
        }

        if (iswallDetect)
        {
            speed = 2;
        }



        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)  // player Jump
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))  // Player Speed Increase
        {
            speedUp = 3;
        }
        else
        {
            speedUp = 2;
        }

        if (Input.GetKeyDown(KeyCode.Z) && dashCoolDownTimer <= 0)
        {
            StartCoroutine(Dash());
            dashCoolDownTimer = 3f;
        }


        CheckWallCollision();

        // playerDashCoolDownTimer
        if(dashCoolDownTimer > 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }
    }


    void CheckWallCollision()
    {
        RaycastHit2D _hit = Physics2D.Raycast(wallCheckRayPoint.position, Vector2.right, wallCheckDistance, wallDetectLayer);
        if (_hit.collider)
        {
            iswallDetect = true;
            rb.AddForce(transform.position * 0.01f, ForceMode2D.Impulse);
            // Debug.Log("WallDetect");
        }
        else
        {
            iswallDetect = false;
        }
    }


    IEnumerator Dash()  // Player Dash
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        if(facingRight)
        {
            Vector3 endPosition = startPosition + transform.right * dashDistance;
            while (elapsedTime < dashDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dashDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = endPosition;
        }
        if (!facingRight)
        {
            Vector3 endPosition = startPosition + -transform.right * dashDistance;
            while (elapsedTime < dashDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dashDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = endPosition;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(wallCheckRayPoint.position, Vector2.right * wallCheckDistance);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("PlayerDetect");
            jumpsRemaining = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;  
        transform.localScale = new Vector3(-transform.localScale.x,1,1);      
    //  transform.Rotate(0f, 180f, 0f);
    }

}
