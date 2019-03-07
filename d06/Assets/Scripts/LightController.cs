using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public MainController mainController;

    float rotationLeft = 360;
    float rotationSpeed = 30;

    void Update()
    {

        float rotation = rotationSpeed * Time.deltaTime;
        if (rotationLeft > rotation)
        {
            rotationLeft -= rotation;
        }
        else
        {
            rotation = rotationLeft;
            rotationLeft = 360;
        }
        transform.Rotate(0, rotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            mainController.detected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            mainController.detected = false;
    }
}
