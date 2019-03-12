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
    private Coroutine coroutine;
    private bool textStarted;

    private void Start()
    {
        detection.fillAmount = 0.0f;
        warningText.text = "";
    }

    public void GameOverMessage()
    {
        gameOver.SetActive(true);
        if (!textStarted)
            StartCoroutine(GameOverAutoText());
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

    private IEnumerator GameOverAutoText()
    {
        textStarted = true;
        string gameOverText = "Mission Has Failed\nReinitializing........";
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
