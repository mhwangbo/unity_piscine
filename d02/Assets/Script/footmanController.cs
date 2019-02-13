using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footmanController : MonoBehaviour
{
    public List<footman> footmanList = new List<footman>();


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                return;
            foreach (footman fm in footmanList)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            }
        } else if (Input.GetMouseButtonDown(1))
        {
            foreach (footman fm in footmanList)
            {
                if (fm.isSelected)
                {
                    fm.isSelected = false;
                }
            }
            footmanList.Clear();
        }
    }
}
