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
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 5.0f && playerController.Shot)
            playerDetected = true;
        if (playerDetected)
        {
            if (!alertSign.activeSelf)
                StartCoroutine(AttackPlayer());
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator AttackPlayer()
    {
        alertSign.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        alertSign.SetActive(false);
        playerDetected = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            StartCoroutine(Killed());
        }
    }

    private IEnumerator Killed()
    {
        yield return new WaitForSeconds(1.0f);
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