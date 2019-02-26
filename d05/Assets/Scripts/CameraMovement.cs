using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject golfBall;
    private Vector3 originalPos;
    private Vector3 originalAng;
    private Vector3 offset;
    private bool locked;

    public float speedH = 20.0f;
    public float speedV = 20.0f;
    public float speedM = 4.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        golfBall = GameObject.Find("GolfBall");
        originalPos = transform.position;
        originalAng = transform.eulerAngles;
        offset = transform.position - golfBall.transform.position;
        locked = true;
    }

    void Update()
    {
        if (!locked)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        if (Input.GetKey("e"))
            moveCamera(Vector3.up);
        if (Input.GetKey("q"))
            moveCamera(Vector3.down);
        if (Input.GetKey("w"))
            moveCamera(Vector3.forward);
        if (Input.GetKey("s"))
            moveCamera(Vector3.back);
        if (Input.GetKey("a"))
            moveCamera(Vector3.left);
        if (Input.GetKey("d"))
            moveCamera(Vector3.right);
        if (Input.GetKeyDown("space"))
        {
            transform.position = originalPos;
            transform.eulerAngles = originalAng;
            locked = true;
        }
    }

    void moveCamera(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * speedM);
        if (locked)
            locked = false;
    }
}
