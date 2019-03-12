using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVController : MonoBehaviour
{
    public MainController mainController;

    private void Start()
    {
        mainController = GameObject.FindGameObjectWithTag("mainController").GetComponent<MainController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            mainController.cctvDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            mainController.cctvDetected = false;
    }
}
