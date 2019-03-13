using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc : MonoBehaviour
{
    public float maxHP;
    public float curHP;
    public float searchDistance = 2.0f;

    public float speed = 1.0f;
    public Vector3 target;
    public Vector3 finalTarget;

    public Animator anim;
    private bool isRight = true;
    private float dirX;
    private float dirY;

    public AudioSource walkingSound;
    public AudioSource attackSound;
    public AudioSource deadSound;

    public GameObject follow;
    private bool isFollow = false;
    private GameObject enemyTownHall;

    private bool attacked = false;
    private bool coroutineStarted = false;
    private bool attacking = false;

    private IEnumerator coroutine;

    private bool protect = false;

    void Start()
    {
        target = transform.position;
        enemyTownHall = GameObject.Find("TownHallHuman");
        finalTarget = enemyTownHall.transform.position;
        anim = GetComponent<Animator>();
        curHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "footman" || collision.gameObject.name == "TownHallHuman")
        {
            anim.SetBool("attack", true);
            attackSound.Play();
        }
        if (attacked && collision.gameObject.name == "Footman")
        {
            coroutine = Damage();
            StartCoroutine(coroutine);
            coroutineStarted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "footman" || collision.gameObject.name == "TownHallHuman")
        {
            if (coroutineStarted && collision.gameObject.tag == "footman")
            {
                attacked = false;
                StopCoroutine(coroutine);
                coroutineStarted = false;
                print("Orc Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
            }
            attacking = false;
            anim.SetBool("attack", false);
        }
    }

    IEnumerator Damage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            curHP--;
        }
    }

    void Attacked()
    {
        if (!attacked)
            attacked = true;
        else
            attacked = false;
    }

    void Protect(bool trueOrFalse)
    {
        protect = trueOrFalse;
    }

    private void SearchForEnemy()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("footman");
        GameObject closest = null;
        float shortest = searchDistance;


        for (int i = 0; i < enemyList.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);
            if (distance < shortest)
            {
                shortest = distance;
                closest = enemyList[i];
            }
        }
        if (closest && shortest > 0.0f && !attacking)
        {
            attacking = true;
            isFollow = true;
            follow = closest;
            walkingSound.Play();
        }
        else if (shortest > 0.0f && attacking)
        {
            
        }
        else
        {
            isFollow = false;
            attacking = false;
        }
    }

    void Update()
    {
        target = finalTarget;
        if (curHP <= 0)
        {
            deadSound.Play();
            print("Orc Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
            Destroy(gameObject);
        }
        else
        {
            if (protect)
            {
                target = GameObject.Find("TownHallOrc").transform.position;
            }
            else
            {
                SearchForEnemy();
                if (!follow)
                {
                    if (isFollow)
                    {
                       isFollow = false;
                       follow = null;
                       anim.SetBool("attack", false);
                       walkingSound.Play();
                    }
                }
                if (isFollow)
                    target = follow.transform.position;
            }
            target.z = transform.position.z;
            dirX = target.x - transform.position.x;
            dirY = target.y - transform.position.y;
            anim.SetBool("walk", true);
            anim.SetFloat("x", dirX);
            anim.SetFloat("y", dirY);
            flip();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                anim.SetBool("walk", false);
            }
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
