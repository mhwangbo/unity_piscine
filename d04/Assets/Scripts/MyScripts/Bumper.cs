using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float x;
    public float y;
    private Sonic sonic;
    public Sprite folded;
    public Sprite unfolded;
    private SpriteRenderer render;
    private AudioSource audio;

    private void Start()
    {
        sonic = GameObject.Find("Sonic").GetComponent<Sonic>();
        render = gameObject.GetComponent<SpriteRenderer>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Sonic")
        {
            sonic.bumper(x, y);
            audio.Play();
            StartCoroutine(Change());
        }
    }

    IEnumerator Change()
    {
        render.sprite = unfolded;
        yield return new WaitForSeconds(0.3f);
        render.sprite = folded;
    }
}
