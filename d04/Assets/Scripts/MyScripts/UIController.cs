using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text timeText;
    public Text ringText;
    public Text gameScore;
    public Sonic sonic;
    public static float timer;
    [HideInInspector] public int enemyKilled;
    [HideInInspector] public bool gameEnd = false;
    private bool wait = false;
    private int scoreDisplay;

    private void Update()
    {
        timer += Time.deltaTime;
        if (gameEnd)
        {
            if (!wait)
            {
                scoreDisplay = ScoreCalculation();
                if (scoreDisplay > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, scoreDisplay);
                timer = 0;
            }
            StartCoroutine(Wait(6.0f));
            if (wait)
            {
                gameScore.gameObject.SetActive(true);
                gameScore.text = scoreDisplay.ToString();
            }
            if (wait && Input.GetKeyDown("return"))
                SceneManager.LoadScene("DataSelect");
        }
        else
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            string time = string.Format("{0:00}:{1:00}", minutes, seconds);
            timeText.text = time;
            ringText.text = sonic.rings.ToString();
        }
    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        wait = true;
    }

    private int ScoreCalculation()
    {
        float score = (500 * enemyKilled) + (100 * sonic.rings) + (20000 - (100 * (timer)));
        return (Mathf.FloorToInt(score));
    }
}
