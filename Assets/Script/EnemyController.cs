using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public float xPosition = 5.0f;
    public float yPosition = 5.0f;
    public bool isSpellAttackEnemy;
    public Transform firePoint;
    public GameObject spellPrefab;
    

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Reverse direction if hitting the positions
        if (transform.position.x > xPosition || transform.position.x < -xPosition || transform.position.y > yPosition || transform.position.y < -yPosition)
        {
            speed *= -1;
        }
    }

    public void SpellAttack()
    {
        if(isSpellAttackEnemy)
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, firePoint.right);
            if (_hit.collider.tag == "player")
            {

            }
        }
    }
}
