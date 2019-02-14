using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        curHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != transform.gameObject.tag)
        {
            coroutine = DamageBuilding();
            StartCoroutine(coroutine);
            coroutineStarted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (coroutineStarted)
        {
            StopCoroutine(coroutine);
            coroutineStarted = false;
        }
    }

    public IEnumerator DamageBuilding()
    {
        while (true)
        {
            curHP--;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (curHP <= 0)
        {
            isDead = true;
            deadSound.Play();
            if (gameObject.tag == "orc")
                print("The Human Team wins");
            else
                print("The Orc Team wins");
            Destroy(gameObject);
        }
        if (timer > waitTime)
        {
            timer -= waitTime;
            if (transform.tag == "orc")
            {
                GameObject summon = (GameObject)Instantiate(orcPrefab, oController.transform.position, oController.transform.rotation);
                summon.transform.parent = oController.transform;
            }
            else
            {
                GameObject summon = (GameObject)Instantiate(footmanPrefab, fmController.transform.position, transform.rotation);
                summon.GetComponent<footman>().fmController = fmController;
                summon.transform.parent = fmController.transform;
            }
        }
    }
}
