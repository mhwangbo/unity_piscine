using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Sonic sonic;
    private AudioSource audio;


    private void Start()
    {
        GameObject s = GameObject.Find("Sonic");
        sonic = s.GetComponent<Sonic>();
        GameObject a = GameObject.Find("Rings");
        audio = a.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "Sonic" && sonic.isRing)
        {
            sonic.rings++;
            audio.Play();
            Destroy(gameObject);
        }
    }
}
