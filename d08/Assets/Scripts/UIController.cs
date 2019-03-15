using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // player info
    public Image hpBar;
    public Text hpText;
    public Image expBar;
    public Text expText;
    public Text level;
    public PlayerController player;
    private int prevLevel;
    public GameObject levelUp;

    // enemy info
    private GameObject enemy;
    private EnemyController enemyController;
    public GameObject enemyDisplay;
    public Image enemyHpBar;
    public Text enemyHpText;
    public Text enemyLevel;
    public Text enemyName;
    private bool tmpDisplay;

    // game over
    public GameObject gameOver;

    // player info panel
    public GameObject displayPanel;
    public Text[] displayInfo;
    public GameObject[] displayButtons;
    private bool tmp;

    private void Start()
    {
        prevLevel = player.stat.Level;
    }

    private void Update()
    {
        if (prevLevel != player.stat.Level)
        {
            prevLevel = player.stat.Level;
            StartCoroutine(LevelUP());
        }
        hpBar.fillAmount = player.curHealth / player.stat.HP;
        hpText.text = "" + Mathf.RoundToInt(player.curHealth);
        expBar.fillAmount = player.stat.EXP / player.stat.RequiredEXP;
        expText.text = player.stat.EXP + " / " + player.stat.RequiredEXP;
        level.text = "" + player.stat.Level;
        if (!enemyDisplay.activeSelf && player.enemySet)
        {
            enemy = player.enemy;
            enemyController = enemy.GetComponent<EnemyController>();
            enemyDisplay.SetActive(true);
        }
        else
            RayCastEnemy();
        if ((player.enemySet || tmpDisplay) && enemyController.enemyState == EnemyController.State.ALIVE)
            EnemyInfo();
        else
            enemyDisplay.SetActive(false);
        if (player.curHealth <= 0)
            gameOver.SetActive(true);
        if (player.stat.Point > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                displayButtons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                displayButtons[i].SetActive(false);
            }
        }
        if (Input.GetKeyDown("c") && !displayPanel.activeSelf)
        {
            tmp = true;
            displayPanel.SetActive(true);
            displayButtons[4].SetActive(false);
        }
        if(displayPanel.activeSelf)
        {
            FillInfo();
            player.Stop(true);
        }
        if (tmp && Input.GetKeyUp("c"))
            displayPanel.SetActive(false);

    }

    public void DisplayInfoPanel(bool trueOrFalse)
    {
        displayPanel.SetActive(trueOrFalse);
        displayButtons[4].SetActive(trueOrFalse);
    }

    public void AddStat(int type)
    {
        if (type == 1)
            player.stat.Strength += 1;
        else if (type == 2)
            player.stat.Agility += 1;
        else if (type == 3)
            player.stat.Constitution += 1;
        player.stat.Point -= 1;
        
    }

    private void FillInfo()
    {
        displayInfo[0].text = player.name;
        displayInfo[1].text = "" + player.stat.Strength;
        displayInfo[2].text = "" + player.stat.Agility;
        displayInfo[3].text = "" + player.stat.Constitution;
        displayInfo[4].text = "" + player.stat.Point;
        displayInfo[5].text = "" + player.stat.Level;
        displayInfo[6].text = "" + player.stat.MinDamage;
        displayInfo[7].text = "" + player.stat.MaxDamage;
        displayInfo[8].text = "" + player.stat.ArmorStat;
        displayInfo[9].text = player.stat.EXP + " / " + player.stat.RequiredEXP;
    }

    private IEnumerator LevelUP()
    {
        levelUp.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        levelUp.SetActive(false);
    }

    private void EnemyInfo()
    {
        enemyHpBar.fillAmount = enemyController.curHealth / enemyController.stat.HP;
        enemyHpText.text = "" + Mathf.RoundToInt(enemyController.curHealth);
        enemyLevel.text = "" + enemyController.stat.Level;
        enemyName.text = enemy.name;
    }

    private void RayCastEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.transform.tag == "Enemy")
            {
                enemy = hit.transform.gameObject;
                enemyController = enemy.GetComponent<EnemyController>();
                tmpDisplay = true;
                enemyDisplay.SetActive(true);
            }
            else
                tmpDisplay = false;
        }
    }
}
