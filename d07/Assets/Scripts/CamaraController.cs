using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform tank;
    private Vector3 offsetPosition;
    private Space offsetPositionSpace = Space.Self;

    private void Start()
    {
        offsetPosition = transform.position - tank.transform.position;
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (tank == null)
            return;
        if (offsetPositionSpace == Space.Self)
            transform.position = tank.TransformPoint(offsetPosition);
        else
            transform.position = tank.position + offsetPosition;

        transform.LookAt(tank);
    }
}
