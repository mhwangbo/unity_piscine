using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool hole;
    [HideInInspector]public bool isSleeping;
    private Rigidbody rb;

    private Vector3 hitSpeed = new Vector3(0, 0, 0);
    private Coroutine coroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Hit(float power)
    {
        rb.AddForce((transform.forward + transform.up) * 20.0f * power, ForceMode.Impulse);
        coroutine = StartCoroutine(CheckMoving());
    }

    public void StopHit()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator CheckMoving()
    {
        while (true)
        {
            if (rb.velocity.x <= 0.01 && rb.velocity.y <= 0.01 && rb.velocity.z <= 0.01)
            {
                isSleeping = true;
                rb.velocity = Vector3.zero;
            }
            else
                isSleeping = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "hole")
            hole = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "hole")
            hole = false;
    }
}
