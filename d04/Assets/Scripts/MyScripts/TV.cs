using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public Sprite destroyedSprite;
    public GameObject ringPrefab;
    private SpriteRenderer renderer;
    private AudioSource audio;
    private Sonic sonic;
    private BoxCollider2D collider;
    private bool pieces;
    private bool superBoots;
    private bool shield;

    private void Start()
    {
        sonic = GameObject.Find("Sonic").GetComponent<Sonic>();
        if (gameObject.name == "PiecesTV")
            pieces = true;
        else if (gameObject.name == "SuperBootsTV")
            superBoots = true;
        else if (gameObject.name == "ShieldTV")
            shield = true;
        audio = GameObject.Find("Stage").GetComponent<AudioSource>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Sonic" && (sonic.isRolling || sonic.isJumpball))
        {
            if (pieces)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector3 rand = Random.insideUnitSphere * 1 + transform.position;
                    rand.y = Mathf.Abs(rand.y) + 1;

                    GameObject ring = (GameObject)Instantiate(ringPrefab, rand, Quaternion.identity);
                }
            }
            if (superBoots)
            {
                StartCoroutine(Faster());
            }
            if (shield && sonic.isShielded == false)
            {
                sonic.isShielded = true;
                GameObject tmp = (GameObject)Instantiate(sonic.currentShield, GameObject.Find("Sonic").transform.position, Quaternion.identity);
                tmp.transform.parent = GameObject.Find("Sonic").transform;
                tmp.SetActive(true);
            }
            renderer.sprite = destroyedSprite;
            collider.isTrigger = true;
            sonic.destroy();
        }
    }

    IEnumerator Faster()
    {
        sonic.maxSpeed = 30;
        audio.pitch = 1.2f;
        yield return new WaitForSeconds(15.0f);
        sonic.maxSpeed = 20;
        audio.pitch = 1.0f;
    }
}
