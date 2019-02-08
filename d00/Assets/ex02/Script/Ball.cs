using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public float deceleration;
    public bool gameEnd = false;
    public bool moving = false;
    public GameObject hole;
    public Vector3 direction;

    void Start()
    {
        direction = Vector3.up;
    }


    void Update()
    {
      if (speed > 0)
      {
        moving = true;
        if (transform.position.y >= 4)
          direction = Vector3.down;
        if (transform.position.y <= -4)
          direction = Vector3.up;
        transform.Translate(direction * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 4.0f), transform.position.z);
        speed -= 0.1f;
      } else {
        moving = false;
      }
      if (transform.position.y >= hole.transform.position.y - 0.3f && transform.position.y <= hole.transform.position.y + 0.3f)
      {
        if (speed < 2.0f)
        {
            gameEnd = true;
            transform.position = new Vector3(100, 100, 0);
        }
      }
    }
}
