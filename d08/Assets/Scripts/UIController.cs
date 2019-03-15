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

    private void Update()
    {
        hpBar.fillAmount = player.curHealth / player.stat.HP;
        hpText.text = "" + Mathf.RoundToInt(player.curHealth);
        expBar.fillAmount = player.stat.EXP / player.stat.RequiredEXP;
        expText.text = player.stat.EXP + " / " + player.stat.RequiredEXP;
        level.text = "" + player.stat.Level;
    }
}
