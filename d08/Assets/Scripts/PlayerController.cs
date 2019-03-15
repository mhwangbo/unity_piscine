using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                print(hit.transform.name);
                agent.isStopped = false;
                agent.destination = hit.point;
                animator.SetBool("running", true);
            }
        }
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= 6.0f)
            {
                agent.isStopped = true;
                animator.SetBool("running", false);
            }
        }
        //if (!agent.pathPending)
        //{
        //    if (agent.remainingDistance <= agent.stoppingDistance)
        //    {
        //        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
        //        {
        //            agent.isStopped = true;
        //            animator.SetBool("running", false);
        //        }
        //    }
        //}
    }
}
