using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    public bool hit;
    public int direction = 0;
    private Vector3 angle;
    private bool gameStart = false;
    public Player playerOne;
    public Player playerTwo;
    [HideInInspector] public bool dead;

    void Begin()
    {
        transform.position = new Vector3(0, 0, 0);
        if (direction == 0)
            {
                angle = new Vector3(-7.5f, Random.Range(-4.5f, 4.5f), 0);

            } else
            {
                angle = new Vector3(7.5f, Random.Range(-4.5f, 4.5f), 0);
            }
            gameStart = true;
            if (dead)
                Debug.Log("Player 1: " + playerOne.score + " | Player 2: " + playerTwo.score);
            dead = false;
    }

    void Update()
    {
        if (!gameStart || dead)
            Begin();
        if (hit)
        {
            angle.x *= -1;
            angle.y = Random.Range(-5, 5);
            transform.Translate(angle * Time.deltaTime * 2);
            hit = false;
        }
        else if (!hit && !dead && gameStart)
        {
            if (transform.position.y > 4.5 || transform.position.y < -4.5)
            {
                angle.y *= -1;
            }
            transform.Translate(angle * Time.deltaTime);
        }
    }
}
