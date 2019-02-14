using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector3 target;
    public Vector3 finalTarget;

    public Animator anim;
    private bool isRight = true;
    private bool attack = false;
    private float dirX;
    private float dirY;

    public AudioSource walkingSound;
    public AudioSource attackSound;

    public GameObject follow;
    private bool isFollow = false;
    private GameObject enemyTownHall;

    void Start()
    {
        target = transform.position;
        enemyTownHall = GameObject.Find("TownHallHuman");
        finalTarget = enemyTownHall.transform.position;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attack && collision.gameObject.name == follow.name)
        {
            target = transform.position;
            anim.SetBool("attack", true);
            attackSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "orc")
            anim.SetBool("attack", false);
    }

    private void SearchForEnemy()
    {
        GameObject enemyList = GameObject.Find("Footman");

    }

    void Update()
    {
        SearchForEnemy();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit || (hit.collider.gameObject.transform.tag != "footman"))
            {
                if (hit)
                {
                    attack = true;
                    follow = hit.collider.gameObject;
                    isFollow = true;
                }
                else
                    isFollow = false;
                walkingSound.Play();
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                dirX = target.x - transform.position.x;
                dirY = target.y - transform.position.y;
                anim.SetBool("walk", true);
                anim.SetFloat("x", dirX);
                anim.SetFloat("y", dirY);
                flip();
            }
        }
        if (isFollow && !follow)
        {
            isFollow = false;
            follow = null;
            anim.SetBool("attack", false);
        }
        if (isFollow)
        {
            target = follow.transform.position;
            dirX = target.x - transform.position.x;
            dirY = target.y - transform.position.y;
            anim.SetFloat("x", dirX);
            anim.SetFloat("y", dirY);
            flip();
            anim.SetBool("walk", true);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            anim.SetBool("walk", false);
        }
    }

    void flip()
    {
        int sign = 1;
        if (isRight && dirX <= 0)
        {
            sign = -1;
            isRight = false;
        }
        else if (!isRight && dirX > 0)
        {
            sign = -1;
            isRight = true;
        }
        Vector3 temp = anim.transform.localScale;
        temp.x *= sign;
        anim.transform.localScale = temp;
    }
}
