using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalEnemy : MonoBehaviour
{
    [SerializeField] int enemies;
    public int waves;
    public bool canSummon;
    public float waveTime;
    public float restTime;

    // UI
    public Text waveText;
    public Text timerText;

    PlayerController playerController;

    private void Start()
    {
        waves = 1;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        waveTime = 45.0f;
        restTime = 10.0f;
        waveText.text = "Wave " + waves;
        timerText.text = waveTime.ToString();
    }

    private void Update()
    {
        if (waveTime > 0.0f)
        {
            waveTime -= Time.deltaTime;
            timerText.text = waveTime.ToString();
            GameObject[] childEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemies = childEnemies.Length;
            if (enemies < 20)
                canSummon = true;
            else
                canSummon = false;
            if (waveTime <= 0.0f)
            {
                timerText.text = restTime.ToString();
                waveText.text = "BREAK TIME";
            }
        }
        else if (restTime > 0.0f)
        {
            canSummon = false;
            restTime -= Time.deltaTime;
            timerText.text = restTime.ToString();
            playerController.hp++;
        }
        else
        {
            waves++;
            waveTime = 45.0f;
            restTime = 10.0f;
            waveText.text = "Wave " + waves;
            timerText.text = waveTime.ToString();
        }

    }
}