using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene("ex00");
    }
}
