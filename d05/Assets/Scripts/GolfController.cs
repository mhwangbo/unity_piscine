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
    public bool isShoot;

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
            if (!uiController.onLockScreen.activeSelf)
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
                    ballController.Hit(uiController.powerLevel);
                    isShoot = true;
                }

            }
        }
        else if (!cameraScript.locked)
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
}
