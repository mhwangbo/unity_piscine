using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedM = 2.5f;
    public MainController mainController;

    // Mouse Rotation Control
    public float speedH = 15.0f;
    public float speedV = 15.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        if (!mainController.gameOver)
        {
            // Mouse
            float tmp = pitch - (speedV * Input.GetAxis("Mouse Y"));
            if (tmp >= -20.0f && tmp <= 20.0f)
                pitch = tmp;
            yaw += speedH * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            // Keyboard
            if (Input.GetKey("w"))
                moveCamera(Vector3.forward);
            if (Input.GetKey("s"))
                moveCamera(Vector3.back);
            if (Input.GetKey("a"))
                moveCamera(Vector3.left);
            if (Input.GetKey("d"))
                moveCamera(Vector3.right);

            // Run
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speedM = 4.5f;
                mainController.run = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speedM = 2.5f;
                mainController.run = false;
            }
        }
    }

    void moveCamera(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * speedM);
        transform.position = new Vector3(transform.position.x, 1.312f, transform.position.z);
    }
}
