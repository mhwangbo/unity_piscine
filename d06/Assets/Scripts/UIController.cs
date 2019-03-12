using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image detection;
    public Text warningText;
    public GameObject warningPanel;
    public GameObject gameOver;
    public Text instruction;
    public GameObject startText;
    private Coroutine coroutine;
    private bool textStarted;

    private void Start()
    {
        detection.fillAmount = 0.0f;
        warningText.text = "";
        StartCoroutine(DisplayText());
    }


    private IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(5.0f);
        startText.SetActive(false);
    }

    public void GameMessage(string str)
    {
        gameOver.SetActive(true);
        if (!textStarted)
            StartCoroutine(AutoText(str));
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

    public void SetInstruction(string str)
    {
        instruction.text = str;
    }

    private IEnumerator AutoText(string str)
    {
        textStarted = true;
        string gameOverText = str;
        Text gameOverMessage = gameOver.GetComponent<Text>();
        for (int printIndex = 0; printIndex < gameOverText.Length; printIndex++)
        {
            gameOverMessage.text = gameOverText.Substring(0, printIndex);
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
