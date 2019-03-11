using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image detection;
    public Text warningText;
    public GameObject warningPanel;
    public GameObject gameOver;
    public GameObject crossHair;
    private Coroutine coroutine; 

    private void Start()
    {
        detection.fillAmount = 0.0f;
        warningText.text = "";
    }

    public void GameOverMessage()
    {
        gameOver.SetActive(true);
    }

    public void SetDetectionBar(float detectionLevel)
    {
        detection.fillAmount = detectionLevel;
    }

    public void StartBlinking(bool trueOrFalse)
    {
        if (trueOrFalse)
            coroutine = StartCoroutine(BlinkText());
        else
        {
            StopCoroutine(coroutine);
            warningText.text = "";
            warningPanel.SetActive(false);
        }
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            warningText.text = "WARNING";
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(.2f);
            warningText.text = "";
            warningPanel.SetActive(false);
            yield return new WaitForSeconds(.2f);
        }
    }
}
