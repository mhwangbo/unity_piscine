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
    private bool isStun;

    public bool IsKilled{ get { return isKilled; }}

    Vector3 prevPos;
    public GameObject stunPanel;

    void Update()
    {
        if (!isKilled && !isStun)
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
            prevPos = transform.position;
        }
        else if (isKilled)
        {
            hpText.text = "0";
            hpBar.fillAmount = 0;
        }
        else if (isStun)
        {
            transform.position = prevPos;
        }

    }

    public void Attacked(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
            isKilled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "EnergyBall")
        {
            Destroy(other.gameObject);
            StartCoroutine(Stun());
        }
    }

    IEnumerator Stun()
    {
        isStun = true;
        stunPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        isStun = false;
        stunPanel.SetActive(false);
    }
}
