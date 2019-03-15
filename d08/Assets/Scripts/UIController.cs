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

    // enemy info
    private GameObject enemy;
    private EnemyController enemyController;
    public GameObject enemyDisplay;
    public Image enemyHpBar;
    public Text enemyHpText;
    public Text enemyLevel;
    public Text enemyName;
    private bool tmpDisplay;

    private void Update()
    {
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
