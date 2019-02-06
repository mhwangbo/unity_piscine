﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonController : MonoBehaviour
{

  private int dead = 0;
  private int breath = 100;

  void Update()
  {
    Vector3 temp;
    if (gameObject.transform.localScale.x < 1.5 && gameObject.transform.localScale.x > 0)
    {
      if (Input.GetKey(KeyCode.Space) && breath > 0)
      {
        temp = new Vector3 (gameObject.transform.localScale.x + 0.03f, gameObject.transform.localScale.y + 0.03f, gameObject.transform.localScale.z);
        breath -= 2;
      }
      else
      {
          temp = new Vector3 (gameObject.transform.localScale.x - 0.015f, gameObject.transform.localScale.y - 0.015f, gameObject.transform.localScale.z);
          if (breath < 50)
            breath += 1;
      }
      gameObject.transform.localScale = temp;
    }
    else
    {
      dead = 1;
      Debug.Log ("Balloon life time: " + Mathf.RoundToInt (Time.time) + "s");
      Destroy(gameObject);
    }
  }
}
