using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Sonic sonic;
    private float timer;
    public float waitTime;
    public float x;
    public float speed;
    private Vector3 initialPosition;
    private Vector3 endLocation;
    private int sign;
    public int health;
    private int healthSave;

    private void Start()
    {
        sonic = GameObject.Find("Sonic").GetComponent<Sonic>();
        initialPosition = transform.position;
        endLocation = new Vector3(initialPosition.x + x, initialPosition.y, initialPosition.z);
        healthSave = health;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void Update()
    {
        if (transform.name == "FixedEnemy")
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                timer -= waitTime;
                GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (transform.position.x >= endLocation.x)
            {
                sign = -1;
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            else if (transform.position.x <= initialPosition.x)
            {
                sign = 1;
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime * sign);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Sonic" && transform.name == "SpikeEnemy")
        {
            if (sonic.isInvincible == false && sonic.isHit == false)
                sonic.getHit();
        }
        else if (collision.transform.name == "Sonic" && (sonic.isRolling || sonic.isJumpball))
        {
            health--;
            if (health == 0)
            {
                sonic.destroy();
                sonic.enemyKilled += healthSave;
                Destroy(gameObject);

            }

        }
        else if (collision.transform.name == "Sonic")
        {
            if (sonic.isInvincible == false && sonic.isHit == false)
                sonic.getHit();
        }
    }
}