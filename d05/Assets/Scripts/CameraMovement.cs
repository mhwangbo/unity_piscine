using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 originalAng;
    private Vector3 offset;
    [HideInInspector] public bool locked;

    public float speedH = 20.0f;
    public float speedV = 20.0f;
    public float speedM = 4.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public GameObject ball;
    public BallController ballScript;
    public GolfController golfController;
    private Vector3 point;
    private Vector3 prevBallPos;

    private void Start()
    {
        offset = transform.position - ball.transform.position;
        locked = true;
        PointCalculation();
        prevBallPos = ball.transform.position;
    }

    private void PointCalculation()
    {
        point = ball.transform.position;
        transform.position = point + offset;
        transform.LookAt(point);
        originalPos = transform.position;
        originalAng = transform.eulerAngles;
    }

    void Update()
    {
        if (ballScript.isSleeping && !ballScript.hole && !ballScript.inWater)
        {
            if (Input.GetKey("e"))
                moveCamera(Vector3.up);
            if (Input.GetKey("q"))
                moveCamera(Vector3.down);
            if (!locked)
            {
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");
                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
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
            else
            {
                if (Input.GetKey("a") || Input.GetKey("d"))
                    transform.RotateAround(point, new Vector3(0f, Input.GetAxis("Horizontal"), 0f), 5 * Time.deltaTime * speedH);
            }
            if (!ballScript.hole && prevBallPos != ball.transform.position)
            {
                PointCalculation();
                prevBallPos = ball.transform.position;
            }
        }
        else
        {
            point = ball.transform.position;
            transform.LookAt(point);
        }
        if (ballScript.isSleeping && golfController.isShoot)
        {
            PointCalculation();
            ballScript.StopHit();
            golfController.isShoot = false;
        }


    }

    void moveCamera(Vector3 direction)
    {
        if (locked)
        {
            locked = false;
        }
        transform.Translate(direction * Time.deltaTime * speedM);
    }
}
