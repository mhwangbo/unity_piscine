using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
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
        isLarged = false;
        coroutine = StartCoroutine(enlarge());
    }
    IEnumerator enlarge()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();

        if (isLarged)
            EiffelTower();
    }
}
