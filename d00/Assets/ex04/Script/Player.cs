using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int player_number;
    public PongBall ball;
    public int score;
    public Player opponent;

    void Update()
    {
        if (player_number == 1)
        {
            if (Input.GetKey("w") && transform.position.y + 2 <= 5)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            else if (Input.GetKey("s") && transform.position.y - 2 >= -5)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 5);
            }
        }
        else if (player_number == 2)
        {
            if (Input.GetKey("up") && transform.position.y + 2 <= 5)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            else if (Input.GetKey("down") && transform.position.y - 2 >= -5)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 5);
            }
        }
        if (player_number == 1 && ball.transform.position.x <= transform.position.x + 0.5 ||
                    player_number == 2 && ball.transform.position.x >= transform.position.x - 0.5)
        {
            if (ball.transform.position.y >= transform.position.y - 2 &&
                     ball.transform.position.y <= transform.position.y + 2)
            {
                ball.hit = true;
            }
            else if (!ball.dead)
            {
                ball.dead = true;
                opponent.score++;
                if (player_number == 2)
                    ball.direction = 1;
                else
                    ball.direction = 0;
            }
        }
    }
}
