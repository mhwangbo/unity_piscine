using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Text startbutton;
    public Text exitbutton;
    public Transform startBG;
    public Transform exitBG;
    private int maxX = 2;
    private int x = 0;
    bool isleft = true;
    //background Color
    public Color[] colors;
    int colorindex = 0;
    public Image background;
    public float colorspeed = 5f;

    //eiffel Tower
    public Image eiffel;
    float time = 0;
    bool isLarged = true;

    private Coroutine coroutine;

    int i = 0;
    void Start()
    {

    }

    public void SetColor(Color color)
    {
        background.color = color;
    }

    public void ColorChange()
    {
        var startColor = background.color;
        var endColor = colors[0];

        if (colorindex < colors.Length - 1)
        {
            endColor = colors[colorindex + 1];
        }

        var newColor = Color.Lerp(startColor, endColor, Time.deltaTime * 2);
        SetColor(newColor);
        if (newColor == endColor)
        {
            if (colorindex + 1 < colors.Length)
            {
                colorindex++;
            }
            else
                colorindex = 0;
        }
    }

    void EiffelTower()
    {
        //Instantiate(eiffel, eiffel.transform.localPosition, Quaternion.identity);
        isLarged = false;
        coroutine = StartCoroutine(enlarge());
    }
    IEnumerator enlarge()
    {

        //float t = 0;
        //while (isLarged)
        //{
        //    Vector3 orgScale = eiffel.transform.localScale;
        //    Vector3 endScale = new Vector3(8.5f, 8.5f, 8.5f);
        //    do
        //    {
        //        eiffel.transform.localScale = Vector3.Lerp(orgScale, endScale, t / 1);
        //        t += Time.deltaTime;
        //        yield return null;
        //    }
        //    while (t <= 1);
        //    isLarged = false;
        //}
        //if (!isLarged)
        //{
        //    eiffel.transform.localScale = new Vector3(1, 1, 1);
        //    eiffel.transform.localPosition = new Vector3(0, -88.1f, 0);
        //    isLarged = true;
        //    yield return null;
        //}
        Vector3 orgScale = eiffel.transform.localScale;
        Vector3 endScale = new Vector3(8.5f, 8.5f, 8.5f);
        float t = 0f;
        while (true)
        {
            eiffel.transform.localScale = Vector3.Lerp(orgScale, endScale, t / 1);
            t += Time.deltaTime;
            if (eiffel.transform.localScale.x >= endScale.x)
            {
                t = 0.0f;
                eiffel.transform.localScale = orgScale;
            }
            yield return new WaitForEndOfFrame();
        }
        //float t = 0;
        //do
        //{
        //    eiffel.transform.localScale = Vector3.Lerp(orgScale, endScale, t / 1);
        //    t += Time.deltaTime;
        //    yield return null;
        //}
        //while (t <= 1);
        ////Destroy(eiffel);
        ////eiffel.transform.localScale = new Vector3(1, 1, 1);
        ////eiffel.transform.localPosition = new Vector3(0, -88.1f, 0);
    }

    void Update()
    {

        //time += Time.deltaTime;
        //if (time < 5)
        //{
        //    EiffelTower();
        //    time = 0;
        //}
        //else
        //StopCoroutine(coroutine);
        if (isLarged)
            EiffelTower();

        ColorChange();
        // EiffelTower();
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
                //startBG.localPosition = Vector3.right * 10f;
                exitbutton.gameObject.transform.Rotate(Vector3.zero);

                if (Input.GetKeyDown(KeyCode.Return))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                StartCoroutine(bgmoving(exitBG));
                StartCoroutine(rotate(exitbutton.gameObject));
                //exitBG.localPosition = Vector3.right * 10f;
                startbutton.gameObject.transform.Rotate(Vector3.zero);
                //startBG.localPosition = Vector3.zero;
                if (Input.GetKeyDown(KeyCode.Return))
                    Application.Quit();
                break;
        }

        IEnumerator rotate(GameObject text)
        {
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

            //float time = 0;
            //bool isLeft = true;

            //time += Time.deltaTime;
            //if (time < 1)
            //{
            //    while (isLeft)
            //    {
            //        text.transform.Rotate(Vector3.forward * 10 * Time.deltaTime);
            //        yield return new WaitForSeconds(10f);
            //        isLeft = false;
            //    }
            //    while (!isLeft)
            //    {
            //        text.transform.Rotate(Vector3.back * 10 * Time.deltaTime);
            //        yield return new WaitForSeconds(10f);
            //        isLeft = true;
            //    }
            //    time = 0;
            //}
        }
            //text.transform.Rotate(0, 0, 0);
            //text.transform.Rotate(0, 0, -5f);
            //yield return new WaitForSeconds(1f);

            //text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targety, 5 * Time.deltaTime);
            //yield return new WaitForSeconds(3f);

            //text.transform.rotation = Quaternion.Slerp(targetx, targety, 5 * Time.deltaTime);
            //yield return new WaitForSeconds(1f);

            //text.transform.localPosition = Vector3.zero;
            //text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targety, 5 * Time.deltaTime);
            //yield return new WaitForSeconds(1f);
            //text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targetx, 5 * Time.deltaTime);
            //yield return new WaitForSeconds(1f);

            //text.transform.Rotate(Vector3.right, 5);
            //bool isFirst = true;
            //if (isFirst)
            //{
            //    isFirst = false;
            //    Quaternion targetx = Quaternion.Euler(0, 0, 10);
            //    text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targetx, 5 * Time.deltaTime);
            //    yield return new WaitForSeconds(1f);
            //}
            //if (!isFirst)
            //{
            //    Quaternion targety = Quaternion.Euler(0, 0, -20);
            //    text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targety, 5 * Time.deltaTime);
            //    yield return new WaitForSeconds(1f);
            //    Quaternion targetx = Quaternion.Euler(0, 0, 20);
            //    text.transform.rotation = Quaternion.Slerp(text.transform.rotation, targetx, 5 * Time.deltaTime);
            //    yield return new WaitForSeconds(1f);
            //}
        //}

        IEnumerator bgmoving(Transform text)
        {
            text.localPosition = Vector3.right * 10;
            yield return new WaitForSeconds(3f);
            text.localPosition = Vector3.zero * 10;
            yield return new WaitForSeconds(3f);
        }
    }
}
