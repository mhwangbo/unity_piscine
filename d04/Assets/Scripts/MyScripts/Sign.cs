using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public UIController uiController;
    private bool endGame = false;
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "Sonic" && !endGame)
        {
            endGame = true;
            uiController.gameEnd = true;
            gameObject.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            sound.Play();
        }
    }
}
