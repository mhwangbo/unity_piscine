using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footmanController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider.tag == "footman")
            {
                footman fm = hit.collider.gameObject.GetComponent<footman>();
                fm.selected = true;
                fm.WhenSelected();
            }
        }
    }
}
