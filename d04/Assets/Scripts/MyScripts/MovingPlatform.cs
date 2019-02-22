using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 initialLocation;
    private Vector3 endLocation;
    private int sign = 1;
    public int endX;
    public int endY;
    public bool left;
    public bool up;
    public bool down;
    public bool right;
    private Vector3 direction;

    void Start()
    {
        initialLocation = transform.position;
        endLocation = new Vector3(initialLocation.x + endX, initialLocation.y + endY, initialLocation.z);
        if (up)
            direction = new Vector3(0, 1, 0);
        if (down)
            direction = new Vector3(0, -1, 0);
        if (left)
            direction = new Vector3(-1, 0, 0);
        if (right)
            direction = new Vector3(1, 0, 0);
    }

    void Update()
    {
        if (left)
        {
            if (transform.position.x <= endLocation.x)
                sign = -1;
            else if (transform.position.x >= initialLocation.x)
                sign = 1;
        }
        if (up)
        {
            if (transform.position.y >= endLocation.y)
                sign = -1;
            else if (transform.position.y <= initialLocation.y)
                sign = 1;
        }
        if (right)
        {
            if (transform.position.x >= endLocation.x)
                sign = -1;
            else if (transform.position.x <= initialLocation.x)
                sign = 1;
        }
        transform.Translate(direction * 3.0f * Time.deltaTime * sign);
    }
}
