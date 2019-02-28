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
        isSleeping = true;
    }

    public void Hit(float powerForward, float powerUp)
    {
        isSleeping = false;
        powerForward *= 20.0f;
        powerUp *= 20.0f;
        rb.AddForce((transform.forward * powerForward + transform.up * powerUp), ForceMode.Impulse);
        coroutine = StartCoroutine(CheckMoving());
    }

    public void StopHit()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator CheckMoving()
    {
        while (!isSleeping)
        {
            yield return new WaitForSeconds(1.0f);
            if (rb.velocity == Vector3.zero)
                isSleeping = true;
            else
                isSleeping = false;
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
