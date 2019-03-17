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

    public bool isPatrol;
    public GameObject nextCheckPoint;
    private Vector3 lastPos;

    private GameObject player;
    private PlayerController playerController;
    private Weapon weapon;
    private bool playerDetected;
    private Animator animator;

    private bool isKilled;

    private Rigidbody2D rb2d;

    private Transform target;

    public AudioClip[] aDeath;

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
        rb2d = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        if (isPatrol)
            animator.SetBool("moving", true);
    }

    private void Update()
    {
        if (!playerController.IsKilled && !playerController.IsWon && !isKilled)
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

                rb2d.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
                StartCoroutine(Shooting());
            }
        }
        else if (playerController.IsKilled)
        {
            animator.SetBool("moving", false);
            playerDetected = false;
        }
        else if (isKilled)
            StartCoroutine(Killed());

        if (isPatrol)
        {
            if (!playerDetected && !isKilled)
            {
                Vector3 diff = nextCheckPoint.transform.position - transform.position;
                if (diff.sqrMagnitude <= 0.1f)
                    nextCheckPoint = nextCheckPoint.GetComponent<CheckPoint>().nextCheckPoint;
                else
                {
                    lookForward();
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, nextCheckPoint.transform.position, step);
                }
            }
        }
    }

    void lookForward()
    {
        Vector3 moveDirection = gameObject.transform.position - lastPos;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }
        lastPos = gameObject.transform.position;
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
        SoundManager.instance.RandomSound(aDeath);
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