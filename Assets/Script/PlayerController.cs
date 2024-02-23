using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, speedUp, jumpSpeed, wallCheckDistance, health;
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public GameObject winPanel, gameOverPanel,bgMusic;
    public SpriteRenderer spriteRenderer;

    float dashCoolDownTimer;
    float elapsedTime = 0f;
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


        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)  // player Jump
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))  // Player Speed Increase
        {
            speedUp = 2.5f;
        }
        else
        {
            speedUp = 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.Z) && dashCoolDownTimer <= 0)
        {
            StartCoroutine(Dash());
            dashCoolDownTimer = 3f;
        }


        // playerDashCoolDownTimer
        if (dashCoolDownTimer > 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }
    }



    IEnumerator Dash()  // Player Dash
    {
        Vector3 startPosition = transform.position;
        if (facingRight)
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


    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("PlayerDetect");
            jumpsRemaining = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10);
            StartCoroutine(HurtEffect());
        }


        if (collision.gameObject.tag == "WinCollider")
        {
            winPanel.SetActive(true);
            bgMusic.SetActive(false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        //  transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator HurtEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject,0.2f);
            gameOverPanel.SetActive(true);
            bgMusic.SetActive(false);
        }
    }
}
