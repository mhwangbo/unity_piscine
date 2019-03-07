using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float speedH = 15.0f;
    public float speedV = 15.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        float tmp = pitch - (speedV * Input.GetAxis("Mouse Y"));
        if (tmp >= -20.0f && tmp <= 20.0f)
            pitch = tmp;
        yaw += speedH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
