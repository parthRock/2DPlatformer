using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float distance, scaleValue;
    // public float xPosition = 5f;
    /// public float yPosition = 5f;
    public bool isSpellAttackEnemy;
    public Transform firePoint;
    public GameObject spellPrefab;
    Vector3 minPos, maxPos;

    float attackCoolDownTimer;
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


        if (isSpellAttackEnemy)  // Fire Spell When Player is Detected
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, direction * firePoint.right, distance);

            if (_hit && _hit.collider.tag == "Player" && attackCoolDownTimer < 0)
            {
                Debug.Log("EnemyDetectPlayer");
                SpellAttack();
                attackCoolDownTimer = 2f;
            }

            if (attackCoolDownTimer > 0)
                attackCoolDownTimer -= Time.deltaTime;

        }


    }


    private void OnDrawGizmos()
    {
        if (firePoint)
            Gizmos.DrawRay(firePoint.position, direction * transform.right * distance);
    }

    public void SpellAttack()
    {
        if(isSpellAttackEnemy)
        {         
           GameObject _fireball = Instantiate(spellPrefab, firePoint.position, Quaternion.identity);
           _fireball.GetComponent<EnemyBulletController>().direction = direction;
           Destroy(_fireball, 2f);           
        }
    }
}
