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

    private void SearchOrc(bool trueOrFalse)
    {
        GameObject[] findOrc = GameObject.FindGameObjectsWithTag("orc");
        for (int i = 0; i < findOrc.Length; i++)
        {
            findOrc[i].SendMessage("Protect", trueOrFalse);
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        yield return new WaitForSeconds(2);
        if (collision.IsTouching(gameObject.GetComponent<Collider2D>()))
        {
            if ((collision.gameObject.tag == "footman" && transform.gameObject.tag == "orcTown")
                || (collision.gameObject.tag == "orc" && transform.gameObject.tag == "humanTown"))
            {
                coroutine = Damage();
                StartCoroutine(coroutine);
                coroutineStarted = true;
                if (transform.gameObject.tag == "orcTown")
                {
                    SearchOrc(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (coroutineStarted)
        {
            if (!isDead)
            {
                if (transform.gameObject.tag == "orcTown")
                    print("Orc Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
                else
                    print("Human Unit [" + curHP + "/" + maxHP + "]HP has been attacked");
            }
            else
            {
                if (transform.gameObject.tag == "orcTown")
                    print("The Human Team wins.");
                else
                    print("The Orc Team wins.");
            }
            StopCoroutine(coroutine);
            coroutineStarted = false;
            if (transform.gameObject.tag == "orcTown")
            {
                SearchOrc(false);
            }
        }
    }

    IEnumerator Damage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            curHP --;
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
            Transform targetTransform;
            GameObject targetPrefab;
            float targetPosition = 0.0f;

            if (transform.tag == "orcTown")
            {
                targetTransform = oController.transform;
                targetPrefab = orcPrefab;
            }
            else
            {
                targetTransform = fmController.transform;
                targetPrefab = footmanPrefab;
            }
            RaycastHit2D hit = Physics2D.Raycast(targetTransform.position, -Vector2.up);
            if (hit.collider != null)
            {
                targetPosition += Random.Range(0.2f, 1.0f);
            }
            GameObject summon = (GameObject)Instantiate(targetPrefab, targetTransform.position, targetTransform.rotation);
            summon.transform.parent = targetTransform;
            summon.transform.position = new Vector3(summon.transform.position.x + targetPosition, summon.transform.position.y);
            summon.name = targetPrefab.name;
            if (transform.tag == "humanTown")
                summon.GetComponent<footman>().fmController = fmController;
        }
    }
}
