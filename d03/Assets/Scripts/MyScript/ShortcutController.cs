using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutController : MonoBehaviour
{
    public Image[] towerImages;
    public GameObject[] towerPrefabs;
    public TowerController[] towerControllers;

    public Image fireBallImage;
    public GameObject fireBallPrefab;
    public FireBallController fireBallController;
    private bool fire = false;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private int i = -1;
    private Vector3 originalPosition;

    void Update()
    {
        if (i == -1)
        {
            if (Input.GetKeyDown("1"))
            {
                i = (towerControllers[0].isDraggable ? 0 : -1);
                originalPosition = towerImages[0].transform.position;
            }
            else if (Input.GetKeyDown("2"))
            {
                i = (towerControllers[1].isDraggable ? 1 : -1);
                originalPosition = towerImages[1].transform.position;
            }
            else if (Input.GetKeyDown("3"))
            {
                i = (towerControllers[2].isDraggable ? 2 : -1);
                originalPosition = towerImages[2].transform.position;
            }
            else if (Input.GetKeyDown("4"))
            {
                if (fireBallController.isDraggable)
                {
                    originalPosition = fireBallImage.transform.position;
                    fire = true;
                }

            }
        }

        if (i != -1 || fire)
        {
            if (fireBallController.isDraggable || towerControllers[i].isDraggable)
            {
                if (i != -1)
                    towerImages[i].transform.position = Input.mousePosition;
                else
                    fireBallImage.transform.position = Input.mousePosition;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (i != -1)
                {
                    towerControllers[i].Place();
                    towerImages[i].transform.position = originalPosition;
                    i = -1;
                }
                else
                {
                    fireBallController.Place();
                    fireBallImage.transform.position = originalPosition;
                    fire = false;
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (i != -1)
                {
                    towerImages[i].transform.position = originalPosition;
                    i = -1;
                }
                else
                {
                    fireBallImage.transform.position = originalPosition;
                    fire = false;
                }
            }
        }
    }
}
