using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject pistol;
    public GameObject rifle;

    public float hp;
    private bool isKilled;

    public bool IsKilled{ get { return isKilled; }}

    void Update()
    {
        if (!isKilled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                rifle.SetActive(true);
                pistol.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                rifle.SetActive(false);
                pistol.SetActive(true);
            }
        }

    }

    public void Attacked(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
            isKilled = true;
    }
}
