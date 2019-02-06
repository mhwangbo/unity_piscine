using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
  public GameObject[] cubePrefabs;
  private float timer = 0.0f;
  private float waitTime = 0.5f;
  public static int[] letter = new int[3];

  void Start() {
    letter[0] = 0;
    letter[1] = 0;
    letter[2] = 0;
  }

    void  Update()
  {
    timer += Time.deltaTime;
    if (timer > waitTime ) {
      timer -= waitTime;
      int random = Random.Range(0, 3);
      if (letter[random] == 0)
      {
            GameObject.Instantiate(cubePrefabs[random]);
            letter[random] = 1;
      }

    }
  }
}
