using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerScript_ex05 : MonoBehaviour
{
    public GameObject[] characters;
    public int characterChoice;

    public float speed;
    public float height;
    private float localSpeed;
    private float localHeight;
    private bool jump = true;

    static bool t_exit = false;
    static bool c_exit = false;
    static bool j_exit = false;

    public GameObject trap;
    public GameObject hole;

    Camera mainCamera;
    static bool cameraMove = true;

    void Start()
    {
        mainCamera = Camera.main;
        localSpeed = speed;
        localHeight = height;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "character" || collision.gameObject.tag == "platform" || collision.gameObject.tag == "button") && collision.contacts.Length > 0)
        {
            jump = true;
        }
        if (collision.gameObject.tag == "trap")
        {
            Debug.Log("You Died");
            Reset();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "red_exit" && transform.name == "thomas")
            t_exit = true;
        if (other.transform.name == "blue_exit" && transform.name == "claire")
            c_exit = true;
        if (other.transform.name == "yellow_exit" && transform.name == "john")
            j_exit = true;
        if (t_exit && c_exit && j_exit)
            Win();
        if (other.transform.name == "Hole")
        {
            cameraMove = false;
        }
        if (other.tag == "bullet")
        {
            if (transform.name == "thomas")
            {
                Debug.Log("You Died");
                Reset();
            }
            else if (transform.name == "claire" || transform.name == "john")
                Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "red_exit" && transform.name == "thomas")
            t_exit = false;
        if (other.transform.name == "blue_exit" && transform.name == "claire")
            c_exit = false;
        if (other.transform.name == "yellow_exit" && transform.name == "john")
            j_exit = false;

    }

    void Win()
    {
        Debug.Log("Stage - COMPLETED");
        t_exit = false;
        c_exit = false;
        j_exit = false;
    }

    void Reset()
    {
        t_exit = false;
        c_exit = false;
        j_exit = false;
        cameraMove = true;
        SceneManager.LoadScene("ex05");
    }

    void CameraMovement()
    {
        if (cameraMove)
            mainCamera.transform.position = new Vector3(characters[characterChoice].transform.position.x, characters[characterChoice].transform.position.y, mainCamera.transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey("1"))
        {
            characterChoice = 0;
            localSpeed = speed;
            localHeight = height;
        }
        if (Input.GetKey("2"))
        {
            characterChoice = 1;
            localSpeed = speed / 2;
            localHeight = height / 2;
        }
        if (Input.GetKey("3"))
        {
            characterChoice = 2;
            localSpeed = speed * 2;
            localHeight = height * 2;
        }
        if (Input.GetKey("r"))
            Reset();
        if (Input.GetKey("right"))
            characters[characterChoice].transform.Translate(Vector3.right * localSpeed * Time.deltaTime);
        if (Input.GetKey("left"))
            characters[characterChoice].transform.Translate(Vector3.left * localSpeed * Time.deltaTime);
        if (jump && Input.GetKeyDown("space"))
        {
            jump = false;
            characters[characterChoice].transform.Translate(Vector3.up * localHeight * Time.deltaTime);
        }
        if (characters[characterChoice].transform.position.y < -10)
        {
            Debug.Log("You Died");
            Reset();
        }
        CameraMovement();
    }
}
