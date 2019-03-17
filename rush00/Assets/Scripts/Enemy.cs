using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] weapons;
    public Sprite[] heads;
    public Sprite[] bodies;
    public SpriteRenderer headRender;
    public SpriteRenderer bodyRender;
    public GameObject alertSign;
    public float speed;
    private GameObject player;
    private PlayerController playerController;
    private Weapon weapon;
    private bool playerDetected;
    private Animator animator;

    private bool isKilled;

    private void Start()
    {
        GameObject w = Instantiate(weapons[Random.Range(0, 5)], transform.Find("Weapon"));
        w.layer = gameObject.layer;
        weapon = w.GetComponent<Weapon>();
        w.GetComponent<SpriteRenderer>().sprite = weapon.Equipped;
        headRender.sprite = heads[Random.Range(0, 12)];
        bodyRender.sprite = bodies[Random.Range(0, 2)];
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerController.IsKilled && !isKilled)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= 10.0f && playerController.Shot)
                playerDetected = true;
            if (playerDetected)
            {
                if (!alertSign.activeSelf)
                    StartCoroutine(AttackPlayer());

                Vector3 diff = player.transform.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                StartCoroutine(Shooting());
            }
        }
        else if (playerController.IsKilled)
        {
            animator.SetBool("moving", false);
            playerDetected = false;
        }
        else if (isKilled)
        {
            StartCoroutine(Killed());
        }
            
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1.0f);
        while (playerDetected)
        {
            yield return new WaitForSeconds(0.5f);
            weapon.Shot();
        }
    }

    private IEnumerator AttackPlayer()
    {
        alertSign.SetActive(true);
        animator.SetBool("moving", true);
        yield return new WaitForSeconds(10.0f);
        alertSign.SetActive(false);
        playerDetected = false;
        animator.SetBool("moving", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            isKilled = true;
           // StartCoroutine(Killed());
        }
    }

    private IEnumerator Killed()
    {
        animator.SetBool("moving", false);
        playerDetected = false;
        transform.Rotate(Vector3.forward * 500f * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            RaycastHit2D hit;

            hit = Physics2D.Linecast(transform.position, player.transform.position, 9);
            if (!hit)
                playerDetected = true;
        }
    }
}