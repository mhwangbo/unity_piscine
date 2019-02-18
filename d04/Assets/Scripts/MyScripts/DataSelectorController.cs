using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataSelectorController : MonoBehaviour
{
    private Text lifeLost;
    private Text bestScore;

    private int modernLevel;
    private int metalLevel;
    private int dubstepLevel;

    public GameObject[] modernRoute;
    public GameObject[] metalRoute;
    public GameObject[] dubstepRoute;

    public GameObject selector;

    private GameObject[][] allRoute = new GameObject[3][];
    private int i = 0;
    private int j = 0;

    void Start()
    {
        allRoute[0] = modernRoute;
        allRoute[1] = metalRoute;
        allRoute[2] = dubstepRoute;

        lifeLost = GameObject.Find("LifeLostText").GetComponent<UnityEngine.UI.Text>();
        bestScore = GameObject.Find("BestScoreText").GetComponent<UnityEngine.UI.Text>();
        lifeLost.text = "" + PlayerPrefs.GetInt("lostLife");
        bestScore.text = "" + PlayerPrefs.GetInt("bestScore");

        modernLevel = PlayerPrefs.GetInt("modernLevel");
        metalLevel = PlayerPrefs.GetInt("metalLevel");
        dubstepLevel = PlayerPrefs.GetInt("dubstepLevel");

        for (int i = modernLevel + 1; i < 4; i++)
            modernRoute[i].SetActive(false);
        for (int i = metalLevel + 1; i < 4; i++)
            metalRoute[i].SetActive(false);
        for (int i = dubstepLevel + 1; i < 4; i++)
            dubstepRoute[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("right"))
                i += (i < 3 ? 1 : -3);
            if (Input.GetKeyDown("left"))
                i -= (i > 0 ? 1 : -3);
            if (Input.GetKeyDown("up"))
                j -= (j > 0 ? 1 : -2);
            if (Input.GetKeyDown("down"))
                j += (j < 2 ? 1 : -2);
            selector.transform.position = allRoute[j][i].transform.position;
            if (allRoute[j][i].activeInHierarchy && Input.GetKeyDown("return"))
            {
                string stageName = allRoute[j][i].transform.name;
                SceneManager.LoadScene(stageName);
            }
        }
    }
}
