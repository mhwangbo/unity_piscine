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

    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (mainController.cctvDetected)
        {
            mainController.detectionLevel -= 0.05f;
        }
    }

    void Update()
    {
        if (!mainController.gameOver)
        {
            // Mouse
            MouseMovement();

            // Keyboard
            KeyboardMovement();

            // Raycasting
            ObjectDetermination();
        }
    }

    private void MouseMovement()
    {
        float tmp = pitch - (speedV * Input.GetAxis("Mouse Y"));
        if (tmp >= -25.0f && tmp <= 25.0f)
            pitch = tmp;
        yaw += speedH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void KeyboardMovement()
    {
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

    private void ObjectDetermination()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            string hitObject = hit.transform.name;
            switch (hitObject)
            {
                case "Fan":
                    mainController.objectVisible = 1;
                    break;
                case "KeyCard":
                    mainController.objectVisible = 2;
                    break;
                case "KeyCardReader":
                    mainController.objectVisible = 3;
                    break;
                case "Target":
                    mainController.objectVisible = 4;
                    break;
                default:
                    mainController.objectVisible = -1;
                    break;
            }
        }
        else
        {
            mainController.objectVisible = -1;
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
