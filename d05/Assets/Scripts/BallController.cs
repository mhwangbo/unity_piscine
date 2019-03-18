using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [HideInInspector] public bool hole;
    public UIController uiController;
    public GolfController golfController;
    [HideInInspector] public bool isSleeping;
    private Rigidbody rb;
    private float time;

    private Vector3 hitSpeed = new Vector3(0, 0, 0);
    private Coroutine coroutine;
    [HideInInspector] public bool inWater;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSleeping = true;
    }

    public void Hit(float powerForward, float powerUp)
    {
        isSleeping = false;
        time = 0;
        rb.drag = 1.0f;
        powerForward *= 20.0f * uiController.powerLevel;
        powerUp *= 20.0f * uiController.powerLevel;
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
            time += Time.deltaTime;
            if (golfController.terrainIndex == 1 && time > 0.20f)
                rb.drag = 10.0f;
            if (time > 0.20f)
                rb.drag = 50.0f;
            yield return new WaitForSeconds(1.0f);
            if (rb.velocity == Vector3.zero)
            {
                isSleeping = true;
            }
            else
                isSleeping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "hole")
            hole = true;
        if (other.gameObject.layer == 4)
            inWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "hole")
            hole = false;
    }
}
