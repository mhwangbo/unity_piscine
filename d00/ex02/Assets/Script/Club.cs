using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    public Ball ball;
    public int  score;
    public float strength;
    private bool ending = false;

    void Update()
    {
        if (Input.GetKey("space") && !ball.gameEnd)
        {
            if (strength < 20.0f)
            {
              strength += 0.3f;
              transform.position = new Vector3(transform.position.x, transform.position.y - 0.005f, transform.position.z);
            }
        }
        else if (strength > 0 && !ball.gameEnd)
        {
              if (score <= -5)
                score += 5;
              Debug.Log("Score: " + score);
              ball.direction = Vector3.up;
              ball.speed = strength;
              strength = 0;
        } else if (!ball.moving && !ball.gameEnd){
          transform.position = new Vector3(transform.position.x, ball.transform.position.y + 0.1f, transform.position.z);
        }
        if (ball.gameEnd && !ending)
        {
          if (score < 0)
            Debug.Log("Score: " + score + " You WON!");
          else
            Debug.Log("Score: " + score + " You LOSE");
          ending = true;
        }
    }
}
