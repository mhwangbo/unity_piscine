using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Text coolTime;
    public Text damages;
    public Text cost;
    public Text range;
    public gameManager gm;
    public towerScript towerS;
    public GameObject towerPrefab;
    private Vector3 originalPosition;
    private bool isDraggable = true;
    private Image color;

    void Start()
    {
        originalPosition = gameObject.transform.position;
        color = GetComponent<Image>();
    }

    private void Update()
    {
        coolTime.text = "" + towerS.fireRate;
        damages.text = "" + towerS.damage;
        cost.text = "" + towerS.energy;
        range.text = "" + towerS.range;
        if (gm.playerEnergy < towerS.energy)
        {
            isDraggable = false;
            color.color = Color.red;
        }
        else
        {
            isDraggable = true;
            color.color = Color.white;
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && (hit.collider.gameObject.transform.tag == "empty"))
            {
                Transform targetTransform = hit.collider.gameObject.transform;
                GameObject tower = (GameObject)Instantiate(towerPrefab, targetTransform.position, targetTransform.rotation);
                gm.playerEnergy -= towerS.energy;
            }
            transform.position = originalPosition;
        }
    }
}
