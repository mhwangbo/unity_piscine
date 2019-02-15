using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("ex01");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
