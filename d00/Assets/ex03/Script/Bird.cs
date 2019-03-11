using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float maxHeight;
    public float jumpHeight;
    public float dropRate;
    public int score;
    public float increment;
    [HideInInspector] public bool isDead = false;

    void Update()
    {
      if (transform.position.y < -2.27 || transform.position.y > 5)
        isDead = true;
      if (!isDead)
      {
        transform.Translate(Vector3.down * Time.deltaTime * dropRate);
        if (score % 30 == 0)
          increment += 0.1f;
        if (Input.GetKeyDown("space"))
        {
          transform.Translate(Vector3.up * Time.deltaTime * jumpHeight);
        }
      }
      else
      {
        Debug.Log("Score: " + score + "\nTime: " + Mathf.RoundToInt(Time.time) + "s");
        Destroy(gameObject);
      }
    }
}
