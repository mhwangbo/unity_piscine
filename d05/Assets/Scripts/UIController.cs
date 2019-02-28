using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image power;
    public Text hole;
    public Text par;
    public Text club;
    public Text shot;
    public GameObject onLockScreen;

    private Coroutine coroutine;

    public float powerLevel;
    private float time = 0f;

    private void Start()
    {
        power.fillAmount = 0;
    }

    public void ClubInfo(int clubType)
    {
        string clubName = "";
        switch(clubType)
        {
            case 4:
                clubName = "putter";
                break;
            case 3:
                clubName = "wedge";
                break;
            case 2:
                clubName = "iron";
                break;
            case 1:
                clubName = "wood";
                break;
        }
        club.text = "club : " + clubName;
    }

    public void ShotInfo(int shotNumber)
    {
        shot.text = "Shot " + shotNumber;
    }

    public void HoleInfo(int holeNumber, int parNumber)
    {
        hole.text = "Hole " + holeNumber;
        par.text = "par " + parNumber;
    }

    public void StartPowerBar()
    {
        coroutine = StartCoroutine(PowerBarFill());
    }

    public void StopPowerBar(bool cancel)
    {
        StopCoroutine(coroutine);
        power.fillAmount = 0;
        if (cancel)
            powerLevel = 0;
        time = 0.0f;
    }

    public IEnumerator PowerBarFill()
    {
        while (true)
        {
           time += Time.deltaTime;
           powerLevel = Mathf.PingPong(time, 1);
           power.fillAmount = powerLevel;
           yield return new WaitForSeconds(0.01f);
        }

    }
}
