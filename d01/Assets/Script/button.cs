using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    // button type: 1 = door/passage open; 2 = create platform; 3 = create teleport;
    public int buttonType;
    public GameObject teleport;
    public GameObject platform;
    public GameObject passageOriginal;
    public GameObject redDoor;
    public GameObject yellowDoor;

    void Start()
    {
        if (passageOriginal)
            passageOriginal.SetActive(true);
        if (platform)
            platform.SetActive(false);
        if (teleport)
            teleport.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character" && collision.contacts.Length > 0)
        {
            if (buttonType == 1)
                passageOriginal.SetActive(false);
            else if (buttonType == 2)
                platform.SetActive(true);
            else if (buttonType == 3)
                teleport.SetActive(true);
            else if (buttonType == 4 && collision.gameObject.layer == 9)
                redDoor.SetActive(false);
            else if (buttonType == 4 && collision.gameObject.layer == 10)
                yellowDoor.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            if (buttonType == 1)
                passageOriginal.SetActive(true);
            else if (buttonType == 2)
                platform.SetActive(false);
            else if (buttonType == 3)
                teleport.SetActive(false);
        }
    }
}
