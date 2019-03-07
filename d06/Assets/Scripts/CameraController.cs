using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedM = 4.0f;

    void Update()
    {
        if (Input.GetKey("w"))
            moveCamera(Vector3.forward);
        if (Input.GetKey("s"))
            moveCamera(Vector3.back);
        if (Input.GetKey("a"))
            moveCamera(Vector3.left);
        if (Input.GetKey("d"))
            moveCamera(Vector3.right);
    }

    void moveCamera(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * speedM);
        transform.position = new Vector3(transform.position.x, 1.312f, transform.position.z);
    }
}
