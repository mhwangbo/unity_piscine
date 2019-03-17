using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Text titleText;
    public Text startbutton;
    public Text exitbutton;
    public Transform startBG;
    public Transform exitBG;
    private int maxX = 2;
    private int x = 0;
    bool isLeft = true;
    private Coroutine titlec;

    void Titlerotate()
    {
        isLeft = false;
        titlec = StartCoroutine(title(titleText.gameObject));
    }

    IEnumerator title(GameObject tit)
    {
        Quaternion targetx = Quaternion.Euler(0, 0, 10);
        Quaternion targety = Quaternion.Euler(0, 0, -10);
        bool left = true;
        while (true)
        {
            if (left)
            {
                tit.transform.rotation = Quaternion.Slerp(tit.transform.rotation, targetx, 5 * Time.deltaTime);
                if (tit.transform.rotation.z > 0.08f)
                {
                    while (tit.transform.rotation.z < 0.09f && tit.transform.rotation.z > 0)
                    {
                        tit.transform.rotation = Quaternion.Slerp(tit.transform.rotation, targety, 5 * Time.deltaTime);
                        left = false;
                    }
                }
            }
            if (!left)
            {
                tit.transform.rotation = Quaternion.Slerp(tit.transform.rotation, targety, 5 * Time.deltaTime);
                if (tit.transform.rotation.z < -0.08f)
                {
                    while (tit.transform.rotation.z > -0.09f && tit.transform.rotation.z < 0)
                    {
                        tit.transform.rotation = Quaternion.Slerp(tit.transform.rotation, targetx, 5 * Time.deltaTime);

                        left = true;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (isLeft)
            Titlerotate();
            
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (x == 0)
                x = 1;
            else
                x = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (x  == 1)
                x = 0;
            else
                x = 1;
        }

        switch (x)
        {
            case 0:
                StartCoroutine(bgmoving(startBG));
                StartCoroutine(rotate(startbutton.gameObject));
                exitbutton.gameObject.transform.Rotate(Vector3.zero);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StopCoroutine(titlec);

                }
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                StartCoroutine(bgmoving(exitBG));
                StartCoroutine(rotate(exitbutton.gameObject));
                startbutton.gameObject.transform.Rotate(Vector3.zero);
                if (Input.GetKeyDown(KeyCode.Return))
                    Application.Quit();
                break;
        }

        IEnumerator rotate(GameObject text)
        {
            bool isleft = true;
            Quaternion targetx = Quaternion.Euler(0, 0, 10);
            Quaternion targety = Quaternion.Euler(0, 0, -10);
            float t = 0;
            while (isleft)
            {
                do
                {
                    text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targetx, 5 * Time.deltaTime);
                    t += Time.deltaTime;
                    yield return null;
                }
                while (t <= 1);
                isleft = false;
            }

            while (!isleft)
            {
                do
                {
                    text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targety, 5 * Time.deltaTime);
                    t += Time.deltaTime;
                    yield return null;
                }
                while (t <= 1);
                isleft = true;
            }
        }

        IEnumerator bgmoving(Transform text)
        {
            text.localPosition = Vector3.right * 10;
            yield return new WaitForSeconds(3f);
            text.localPosition = Vector3.zero * 10;
            yield return new WaitForSeconds(3f);
        }
    }
}
