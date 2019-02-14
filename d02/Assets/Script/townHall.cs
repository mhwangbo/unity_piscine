using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class townHall : MonoBehaviour
{
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

    private float timer = 0.0f;
    private float waitTime = 10.0f;

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
            if (collision.gameObject.tag != transform.gameObject.tag)
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
            if (!isDead)
            {
                if (transform.gameObject.tag == "orc")
                    print("Orc Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
                else
                    print("Human Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
            }
            else
            {
                if (transform.gameObject.tag == "orc")
                    print("The Human Team wins.");
                else
                    print("The Orc Team wins.");
            }
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

    public void IncreaseWaitTime()
    {
        waitTime += 2.5f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (curHP <= 0)
        {
            isDead = true;
            deadSound.Play();
            SceneManager.LoadScene("battleField");
            Destroy(gameObject);
        }
        if (timer > waitTime)
        {
            timer -= waitTime;
            if (transform.tag == "orc")
            {
                GameObject summon = (GameObject)Instantiate(orcPrefab, oController.transform.position, oController.transform.rotation);
                summon.transform.parent = oController.transform;
                summon.name = orcPrefab.name;
            }
            else
            {
                GameObject summon = (GameObject)Instantiate(footmanPrefab, fmController.transform.position, transform.rotation);
                summon.GetComponent<footman>().fmController = fmController;
                summon.transform.parent = fmController.transform;
                summon.name = footmanPrefab.name;
            }
        }
    }
}
