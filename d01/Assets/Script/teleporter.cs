using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour
{
    public GameObject teleportOut;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            other.transform.position = teleportOut.transform.position;
        }
    }
}
