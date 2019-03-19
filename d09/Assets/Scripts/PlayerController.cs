using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject pistol;
    public GameObject rifle;

    // UI
    public Text hpText;
    public Image hpBar;

    public float hp;
    private bool isKilled;

    public bool IsKilled{ get { return isKilled; }}

    void Update()
    {
        if (!isKilled)
        {
            hpText.text = hp.ToString();
            hpBar.fillAmount = hp / 100;
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
        else
        {
            hpText.text = "0";
            hpBar.fillAmount = 0;
        }

    }

    public void Attacked(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
            isKilled = true;
    }
}
