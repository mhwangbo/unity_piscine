using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVController : MonoBehaviour
{
    public MainController mainController;

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
