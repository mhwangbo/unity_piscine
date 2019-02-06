using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float speed;

    void Start() {
      speed = Random.Range(2f, 4f);
    }
    void Update()
    {
      float precision = transform.position.y + 4;

      transform.Translate(Vector3.down * speed * Time.deltaTime);
      if (transform.position.y < -4.5)
      {
        if (tag == "a")
          CubeSpawner.letter[0] = 0;
        else if (tag == "s")
          CubeSpawner.letter[1] = 0;
        else if (tag == "d")
          CubeSpawner.letter[2] = 0;
        Destroy(transform.gameObject);
      }
      else if (tag == "a" && Input.GetKeyDown("a") && transform.position.y >= -4)
      {
        Debug.Log("Precision: " + precision);
        CubeSpawner.letter[0] = 0;
        Destroy(transform.gameObject);
      }
      else if (tag == "s" && Input.GetKeyDown("s") && transform.position.y >= -4)
      {
        Debug.Log("Precision: " + precision);
        CubeSpawner.letter[1] = 0;
        Destroy(transform.gameObject);
      }
      else if (tag == "d" && Input.GetKeyDown("d") && transform.position.y >= -4)
      {
        Debug.Log("Precision: " + precision);
        CubeSpawner.letter[2] = 0;
        Destroy(transform.gameObject);
      }
    }
}
