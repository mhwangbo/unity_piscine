using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
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
            rotationLeft = 0;
        }
        transform.Rotate(0, rotation, 0);
    }
}
