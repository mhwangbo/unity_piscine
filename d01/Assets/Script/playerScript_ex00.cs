using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour
{
    public GameObject[] characters;
    public int         characterChoice;
    public float speed;
    public float height;
    Camera mainCamera;

    void Start()
    {
      mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey("1"))
          characterChoice = 0;
        if (Input.GetKey("2"))
          characterChoice = 1;
        if (Input.GetKey("3"))
          characterChoice = 2;
        if (Input.GetKey("r"))
            SceneManager.LoadScene("ex00");
        if (Input.GetKey("right"))
          characters[characterChoice].transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey("left"))
          characters[characterChoice].transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey("space"))
          characters[characterChoice].transform.Translate(Vector3.up * height * Time.deltaTime);
        mainCamera.transform.position = new Vector3(characters[characterChoice].transform.position.x, characters[characterChoice].transform.position.y, mainCamera.transform.position.z);
    }
}
