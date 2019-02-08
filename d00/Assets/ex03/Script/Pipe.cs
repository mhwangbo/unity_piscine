using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    static private float speed = 1.0f;
    public Bird bird;
    bool scored;

    void Update()
    {
        if (!bird.isDead) {
            transform.Translate(Vector3.left * (speed + bird.increment) * Time.deltaTime);
            if (transform.position.x < -8.0f)
            {
              transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
              scored = false;
            }
            if (transform.position.x > -5.0f && transform.position.x < -4.0f)
            {
              if (bird.transform.position.y > 3.23f || bird.transform.position.y < 0.36f)
              {
                if (scored)
                  bird.score -= 5;
                bird.isDead = true;
              }
              else if (!scored)
              {
                bird.score += 5;
                scored = true;
              }
            }
        }
    }
}
