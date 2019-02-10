using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour
{
    public GameObject[] characters;
    public int characterChoice;

    public float speed;
    public float height;
    private float localSpeed;
    private float localHeight;
    private bool jump = true;

    static bool t_exit;
    static bool c_exit;
    static bool j_exit;
 
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        localSpeed = speed;
        localHeight = height;
        t_exit = false;
        c_exit = false;
        j_exit = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "character" || collision.gameObject.tag == "platform") && collision.contacts.Length > 0)
        {
            jump = true;
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
        {
            Debug.Log("You solved it!");
            Application.LoadLevel(0);
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
            Application.LoadLevel(0);
        if (Input.GetKey("right"))
            characters[characterChoice].transform.Translate(Vector3.right * localSpeed * Time.deltaTime);
        if (Input.GetKey("left"))
            characters[characterChoice].transform.Translate(Vector3.left * localSpeed * Time.deltaTime);
        if (jump && Input.GetKeyDown("space"))
        {
            jump = false;
            characters[characterChoice].transform.Translate(Vector3.up * localHeight * Time.deltaTime);
        }
        mainCamera.transform.position = new Vector3(characters[characterChoice].transform.position.x, characters[characterChoice].transform.position.y, mainCamera.transform.position.z);
    }
}
