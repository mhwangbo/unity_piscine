using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Sonic sonic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sonic.isInvincible == false && sonic.isHit == false)
            sonic.getHit();
    }
}
