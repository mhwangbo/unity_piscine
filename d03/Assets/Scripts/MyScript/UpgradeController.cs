using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
             RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
             if (hit && hit.collider.gameObject.tag == "tower")
             {

             }
        }

    }
}
