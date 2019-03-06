using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour
{
    public GameObject scorePanel;
    public Text[] scoreText;

    public void Activate()
    {
        scorePanel.SetActive(true);
    }

    public void DeActivate()
    {
        scorePanel.SetActive(false);
    }

    public void UpdateScore(int score, int hole)
    {
        scoreText[hole - 1].text = "" + score;
    }
}
