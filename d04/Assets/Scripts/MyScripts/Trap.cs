using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Sonic sonic;
    private bool activated;
    private Vector3 initialPos;
    private GameObject boss;
    private Enemy bossScript;

    // Start is called before the first frame update
    void Start()
    {
        sonic = GameObject.Find("Sonic").GetComponent<Sonic>();
        initialPos = transform.position;
        boss = GameObject.Find("Boss");
        bossScript = boss.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && transform.position.y <= 17.82)
        {
            transform.Translate(Vector3.up * 3.0f * Time.deltaTime);
        }
        if (transform.position.y >= 17.82 && activated)
        {
            bossScript.speed = 5;
        }
        if (!boss)
        {
            GameObject.Find("RealSign").SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activated)
        {
            activated = true;
        }
    }
}
