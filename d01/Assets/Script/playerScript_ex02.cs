using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex02 : MonoBehaviour
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

    public GameObject door;
    static bool t_door = false;
    static bool c_door = false;
    static bool j_door = false;

    static bool firstStage = false;
 
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        localSpeed = speed;
        localHeight = height;
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
            Win();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "red_exit" && transform.name == "thomas")
            t_exit = false;
        if (other.transform.name == "blue_exit" && transform.name == "claire")
            c_exit = false;
        if (other.transform.name == "yellow_exit" && transform.name == "john")
            j_exit = false;

        if (other.transform.name == "DoorCollider" && transform.name == "thomas")
            t_door = true;
        if (other.transform.name == "DoorCollider" && transform.name == "claire")
            c_door = true;
        if (other.transform.name == "DoorCollider" && transform.name == "john")
            j_door = true;
        if (t_door && c_door && j_door)
            DoorClose();
    }

    void DoorClose()
    {
        door.transform.Translate(Vector3.up * -2);
    }

    void Win()
    {
        Debug.Log("You solved it!");
        if (!firstStage)
        {
            firstStage = true;
            t_exit = false;
            c_exit = false;
            j_exit = false;

            door.transform.Translate(Vector3.up * 2);
        }
    }

    void Reset()
    {
        Application.LoadLevel(0);
        t_exit = false;
        c_exit = false;
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
        mainCamera.transform.position = new Vector3(characters[characterChoice].transform.position.x, characters[characterChoice].transform.position.y, mainCamera.transform.position.z);
    }
}
