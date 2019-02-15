using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoardController : MonoBehaviour
{
    public gameManager gm;
    public Text gameStatus;
    public Text score;
    public Text buttonText;
    public Text rank;

    private void OnEnable()
    {
        score.text = "" + gm.score;
        ScoreCalculation();
        if (gm.gameEnd == 1)
        {
            buttonText.text = "retry";
            gameStatus.text = "game over";
        }
        else if (gm.gameEnd == 2)
        {
            buttonText.text = "next level";
            gameStatus.text = "you won!";
        }
    }

    private void ScoreCalculation()
    {
        int hp = gm.playerHp;
        int energy = gm.playerEnergy;

        if (energy > 400)
            energy = 400;

        float rankCalc = (float)((hp * 500) + energy) / (float)(400 + (gm.playerMaxHp * 500));
        print(rankCalc);

        if (rankCalc > 0.95)
            rank.text = "S";
        else if (rankCalc > 0.80)
            rank.text = "A";
        else if (rankCalc > 0.70)
            rank.text = "B";
        else if (rankCalc > 0.60)
            rank.text = "C";
        else if (rankCalc > 0.50)
            rank.text = "D";
        else
            rank.text = "F";
    }

    public void buttonAction(string scene)
    {
        if (gm.gameEnd == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (gm.gameEnd == 2)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
