using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text notification;

    PlayerController playerController;

    // Boss
    public GameObject bossSpawner;
    public GameObject boss;
    bool bossWave;

    private void Start()
    {
        waves = 1;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        waveTime = 45.0f;
        restTime = 20.0f;
        waveText.text = "Wave " + waves;
        timerText.text = Mathf.CeilToInt(waveTime) + " s";
    }

    private void Update()
    {
        if (!bossWave && waves % 3 == 0)
        {
            bossWave = true;
            GameObject boss1 = Instantiate(boss, bossSpawner.transform);
            boss1.name = "Boss";
            boss1.transform.localPosition = Vector3.zero;
        }
        if (!playerController.IsKilled)
        {
            if (waveTime > 0.0f)
            {
                waveTime -= Time.deltaTime;
                timerText.text = Mathf.CeilToInt(waveTime).ToString();
                GameObject[] childEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                enemies = childEnemies.Length;
                if (bossWave)
                    canSummon = false;
                else if (enemies < 20)
                    canSummon = true;
                else
                    canSummon = false;
                if (waveTime <= 0.0f)
                {
                    canSummon = false;
                    timerText.text = Mathf.CeilToInt(restTime) + " s";
                    waveText.text = "BREAK TIME";
                    StartCoroutine(Notice("break time began"));
                }
            }
            else if (restTime > 0.0f)
            {
                canSummon = false;
                restTime -= Time.deltaTime;
                timerText.text = Mathf.CeilToInt(restTime) + " s";
                if (playerController.hp < 100)
                    playerController.hp++;
            }
            else
            {
                waves++;
                waveTime = 45.0f;
                restTime = 10.0f;
                waveText.text = "Wave " + waves;
                timerText.text = waveTime + " s";
                StartCoroutine(Notice("new wave began"));
            }
        }
        else
        {
            notification.text = "You passed " + (waves - 1) + " waves";
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private IEnumerator Notice(string str)
    {
        notification.text = str;
        yield return new WaitForSeconds(2.0f);
        notification.text = "";
    }
}