using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform goal;
    public GameObject cannon;
    private NavMeshAgent agent;
    private GameObject target;
    private bool locked;
    private Vector3 cannonOrig;

    private bool shooting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        cannonOrig = cannon.transform.eulerAngles;
    }

    private void Update()
    {
        if(locked)
        {
            agent.isStopped = true;
            if (!shooting)
            {
                shooting = true;
            }
        }
    }

    private IEnumerator ShootTarget()
    {
        while (true)
        {
            Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Enemy")
        {
            cannon.transform.LookAt(other.transform.position);
            cannon.transform.eulerAngles = new Vector3(cannonOrig.x, cannon.transform.eulerAngles.y, 0.0f);
            RayCastFocus();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Enemy")
        {
            target = null;
            locked = false;
            agent.isStopped = false;
        }
    }

    private void RayCastFocus()
    {
        Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(cannon.transform.position, fwd, out hit, 50.0f))
        {
            if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
            {
                target = hit.transform.gameObject;
                locked = true;
            }
            else
            {
                agent.destination = target.transform.position;
                agent.isStopped = false;
            }
        }
    }

}
