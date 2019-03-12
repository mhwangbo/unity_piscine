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

    // Walking Sound
    public AudioSource footStep;
    private bool footStepPlay;
    private Coroutine coroutine;
    private Rigidbody rb;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (mainController.cctvDetected)
        {
            mainController.detectionLevel -= 0.02f;
        }
    }

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
            else if (Input.GetKey("s"))
                moveCamera(Vector3.back);
            else if (Input.GetKey("a"))
                moveCamera(Vector3.left);
            else if (Input.GetKey("d"))
                moveCamera(Vector3.right);
            else if (footStepPlay)
            {
                StopCoroutine(coroutine);
                footStepPlay = false;
                isMoving = false;
                speedM = 2.5f;
                mainController.run = false;
            }

            // Run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isMoving)
                {
                    speedM = 4.5f;
                    mainController.run = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speedM = 2.5f;
                mainController.run = false;
            }
        }
    }

    private IEnumerator PlayFootStep()
    {
        while (true)
        {
            if (!mainController.run)
            {
                footStep.Play();
                yield return new WaitForSeconds(.6f);
            }
            else
            {
                footStep.Play();
                yield return new WaitForSeconds(.3f);
            }
        }

    }

    void moveCamera(Vector3 direction)
    {
        isMoving = true;
        transform.Translate(direction * Time.deltaTime * speedM);
        transform.position = new Vector3(transform.position.x, 1.312f, transform.position.z);
        if (!footStepPlay)
        {
            footStepPlay = true;
            coroutine = StartCoroutine(PlayFootStep());
        }
    }
}
