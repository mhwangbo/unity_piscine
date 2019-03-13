using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    // tank movement
    public float speedM = 5.0f;
    public float speedR = 60.0f;

    // cannon movement
    private float yaw = 0.0f;
    public float speedH = 20.0f;
    public GameObject cannon;
    private Vector3 cannonOriginal;

    private void Start()
    {
        cannonOriginal = cannon.transform.eulerAngles;
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        cannon.transform.eulerAngles = new Vector3(cannonOriginal.x, yaw, 0.0f);
        if (Input.GetKey("w"))
            transform.Translate(Vector3.forward * Time.deltaTime * speedM);
        if (Input.GetKey("s"))
            transform.Translate(Vector3.back * Time.deltaTime * speedM);
        if (Input.GetKey("a"))
            transform.Rotate(0, Input.GetAxis("Horizontal") * speedR * Time.deltaTime, 0);
        if (Input.GetKey("d"))
            transform.Rotate(0, Input.GetAxis("Horizontal") * speedR * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.LeftShift))
            speedM = 10.0f;
        else
            speedM = 5.0f;
        if (Input.GetKey("r"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
