using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public Text flashingText;
    public GameObject ResetText;

    void Start()
    {
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "press enter to play";
            yield return new WaitForSeconds(.5f);
            flashingText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            SceneManager.LoadScene("DataSelect");
        }
    }

    public void ResetButton()
    {
        PlayerPrefs.SetInt("lostLife", 0);
        PlayerPrefs.SetInt("bestScore", 0);
        PlayerPrefs.SetInt("ringLost", 0);
        PlayerPrefs.SetInt("modernLevel", 0);
        PlayerPrefs.SetInt("metalLevel", 0);
        PlayerPrefs.SetInt("dubstepLevel", 0);
        PlayerPrefs.SetInt("AngelIsland", 0);
        PlayerPrefs.SetInt("ChemicalPlant", 0);
        PlayerPrefs.SetInt("FlyingBattery", 0);
        PlayerPrefs.SetInt("OilOcean", 0);
        ResetText.SetActive(true);
    }
}
