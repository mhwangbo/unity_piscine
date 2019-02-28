using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfController : MonoBehaviour
{
    public CameraMovement cameraScript;
    public UIController uiController;
    public BallController ballController;
    public GameObject ball;
    public GameObject arrow;
    private bool isPowerBarStarted;
    [HideInInspector]public bool isShoot;
    private int shotNumber;
    private int holeNumber;
    private int clubNumber;

    private float forward;
    private float up;

    private void Start()
    {
        forward = 2.0f;
        up = 0.8f;
        clubNumber = 1;
    }

    void Update()
    {
        if (cameraScript.locked)
        {
            if (ballController.isSleeping)
            {
                Quaternion ballRotation = Camera.main.transform.rotation;
                ballRotation.x = 0;
                ballRotation.z = 0;

                ball.transform.rotation = Quaternion.RotateTowards(ball.transform.rotation, ballRotation, 20.0f);

            }
            else
            {
                TurnOffUI();
            }
            if (ballController.isSleeping && !uiController.onLockScreen.activeSelf)
            {
                uiController.onLockScreen.SetActive(true);
                arrow.SetActive(true);
            }
            if (Input.GetKeyDown("space"))
            {
                if (!isPowerBarStarted)
                {
                    uiController.StartPowerBar();
                    isPowerBarStarted = true;
                }
                else
                {
                    ballController.Hit(forward, up);
                    uiController.ShotInfo(++shotNumber);
                    isShoot = true;
                }

            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown("r"))
            {
                SetClubInfo();
            }
        }
        else if (!cameraScript.locked)
        {
            TurnOffUI();
        }
    }

    private void SetClubInfo()
    {
        if (clubNumber < 4)
            clubNumber++;
        else
            clubNumber = 1;
        uiController.ClubInfo(clubNumber);
        switch (clubNumber)
        {
            case 1:
                forward = 2.0f;
                up = 0.8f;
                break;
            case 2:
                forward = 1.5f;
                up = 1.5f;
                break;
            case 3:
                forward = 0.8f;
                up = 3.0f;
                break;
            case 4:
                forward = 1.0f;
                up = 0.0f;
                break;
        }
    }

    private void TurnOffUI()
    {
        uiController.onLockScreen.SetActive(false);
        if (isPowerBarStarted)
        {
            uiController.StopPowerBar(true);
            isPowerBarStarted = false;
        }
        arrow.SetActive(false);
    }
}
