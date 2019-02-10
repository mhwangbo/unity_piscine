using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    private Vector3 initialLocation;
    private Vector3 endLocation;
    private GameObject character;
    private int sign = 1;
    static bool contact = false;

    void Start()
    {
        initialLocation = transform.position;
        endLocation = new Vector3(initialLocation.x - 7, initialLocation.y, initialLocation.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            character = collision.gameObject;
            contact = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
            contact = false;
    }

    void Update()
    {
        if (transform.position.x <= endLocation.x)
            sign = -1;
        else if (transform.position.x >= initialLocation.x)
            sign = 1;
        transform.Translate(Vector3.left * 3.0f * Time.deltaTime * sign);
        if (contact)
            character.transform.Translate(Vector3.left * 3.0f * Time.deltaTime * sign);
    }
}
