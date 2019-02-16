using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuBarController : MonoBehaviour
{
    public Text life;
    public Text energy;
    public gameManager gm;
    public bool pauseMenu = false;
    public GameObject pauseMenuPanel;
    public GameObject confirmationMenuPanel;
    public GameObject scoreBoardPanel;

    private int exit = 0;
    private bool paused = false;

    public void Pause()
    {
        gm.pause(true);
        paused = true;
    }

    public void Resume(float speed)
    {
        if (pauseMenu)
        {
            pauseMenu = false;
            pauseMenuPanel.SetActive(false);
            if (exit == 1)
            {
                confirmationMenuPanel.SetActive(false);
                exit = 0;
            }
            gm.pause(false);
        }
        else
        {
            if (paused)
            {
                gm.pause(false);
                paused = false;
            }
            gm.changeSpeed(speed);
        }
    }

    public void Exit()
    {
        if (exit == 0)
        {
            exit++;
            confirmationMenuPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("ex00");
        }
    }

    private void Update()
    {
        life.text = "" + gm.playerHp;
        energy.text = "" + gm.playerEnergy;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            pauseMenuPanel.SetActive(true);
            pauseMenu = true;
        }
        if (gm.gameEnd > 0)
            scoreBoardPanel.SetActive(true);
    }
}