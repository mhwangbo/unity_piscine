using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public townHall th;
    public float maxHP;
    public float curHP;

    private IEnumerator coroutine;
    public bool isDead = false;
    public bool coroutineStarted = false;

    public GameObject orcPrefab;
    public GameObject footmanPrefab;
    public footmanController fmController;
    public orcController oController;

    public AudioSource deadSound;

    private void OnMouseDown()
    {
        enabled = true;
    }

    void Start()
    {
        curHP = maxHP;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        yield return new WaitForSeconds(2);
        if (collision.IsTouching(gameObject.GetComponent<Collider2D>()))
        {
            if ((collision.gameObject.tag == "footman" && transform.gameObject.tag == "orcTown")
    || (collision.gameObject.tag == "orc" && transform.gameObject.name == "TownHallHuman"))
            {
                coroutine = Damage();
                StartCoroutine(coroutine);
                coroutineStarted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (coroutineStarted)
        {
            StopCoroutine(coroutine);
            coroutineStarted = false;
            if (transform.gameObject.tag == "orcTown")
                print("Orc Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
            else
                print("Human Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
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

    void Update()
    {
        if (curHP <= 0)
        {
            isDead = true;
            deadSound.Play();
            th.IncreaseWaitTime();
            Destroy(gameObject);
        }
    }
}
