using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireBallController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public gameManager gm;
    public GameObject fireBallPrefab;
    public bool isDraggable = true;

    private Vector3 originalPosition;
    private Image color;

    private float coolTime = 5.0f;
    private float waitTime = 0.0f;
    private bool ready = true;

    void Start()
    {
        originalPosition = gameObject.transform.position;
        color = GetComponent<Image>();
    }

    private void Update()
    {
        if (!ready)
        {
            waitTime += Time.deltaTime;
            isDraggable = false;
            color.color = Color.red;
        }
        else
        {
            isDraggable = true;
            color.color = Color.white;
        }
        if (waitTime >= coolTime)
        {
            ready = true;
            waitTime = 0.0f;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Place();
        }
    }

    public void Place()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            Transform targetTransform = hit.collider.gameObject.transform;
            GameObject tower = (GameObject)Instantiate(fireBallPrefab, targetTransform.position, targetTransform.rotation);
        }
        transform.position = originalPosition;
        ready = false;
    }
}
