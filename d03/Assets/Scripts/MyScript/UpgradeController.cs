using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public GameObject panel;
    public gameManager gm;

    private GameObject tower;
    private GameObject upgradeTower;
    private GameObject downgradeTower;

    public GameObject upgradeButton;

    public Text upgradeText;
    public Text downgradeText;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.gameObject.tag == "tower")
            {
                tower = hit.transform.gameObject;
                panel.SetActive(true);
                panel.transform.position = Input.mousePosition;
                Reload();
            }
        }

    }

    void Reload()
    {
        if (upgradeTower = tower.GetComponent<towerScript>().upgrade)
            upgradeText.text = "" + upgradeTower.GetComponent<towerScript>().energy;
        else
            upgradeTower = null;
        downgradeTower = tower.GetComponent<towerScript>().downgrade;
        downgradeText.text = "" + (tower.GetComponent<towerScript>().energy / 2);
        if (!upgradeTower || upgradeTower.GetComponent<towerScript>().energy > gm.playerEnergy)
            upgradeButton.SetActive(false);
        else
            upgradeButton.SetActive(true);
    }

    public void Cancel()
    {
        panel.SetActive(false);
        tower = null;
    }

    public void Upgrade()
    {
        if (upgradeTower)
        {
            GameObject newTower = Instantiate(upgradeTower, tower.transform.position, Quaternion.identity);
            Destroy(tower);
            tower = newTower;
            gm.playerEnergy -= tower.GetComponent<towerScript>().energy;
            Reload();
        }

    }

    public void Downgrade()
    {
        if (downgradeTower)
        {
            GameObject newTower = Instantiate(downgradeTower, tower.transform.position, Quaternion.identity);
            gm.playerEnergy += tower.GetComponent<towerScript>().energy / 2;
            Destroy(tower);
            tower = newTower;
            Reload();
        }
        else
        {
            gm.playerEnergy += tower.GetComponent<towerScript>().energy / 2;
            Destroy(tower);
            Cancel();
        }
    }
}
