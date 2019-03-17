using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public PlayerController playerController;
    public Text weaponName;
    public Text bullet;
    public Text type;
    public GameObject gameOver;
    public GameObject Won;
    public Weapon currentWeapon;

    void Start()
    {
        gameOver.SetActive(false);
        Won.SetActive(false);
        ResetText();
    }

    void ResetText()
    {
        weaponName.text = "No weapon";
        bullet.text = "-";
        type.text = "";
    }

    void Update()
    {
        if (playerController.IsKilled)
            gameOver.SetActive(true);
        if (playerController.EquipWeapon)
        {
            weaponName.text = playerController.CurWeapon.name;
            bullet.text = currentWeapon.Ammo.ToString();
            type.text = currentWeapon.Type;
        }
        if (playerController.IsWon)
            Won.SetActive(true);
    }
}
