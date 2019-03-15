using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LambentController : MonoBehaviour
{
    private float hp = 3.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject target;
    private bool targetFound;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Attacked()
    {
        hp--;
    }

    private void Update()
    {
        if (targetFound)
        {
            agent.destination = target.transform.position;
            if (agent.remainingDistance <= 6.0f)
            {
                transform.LookAt(target.transform);
                agent.isStopped = true;
                animator.SetBool("running", false);
                animator.SetBool("attack", true);
            }
            else if (agent.remainingDistance > 6.0f)
            {
                agent.isStopped = false;
                animator.SetBool("attack", false);
                animator.SetBool("running", true);
            }
        }

        //if (!agent.pathPending)
        //{
        //    if (agent.remainingDistance <= 5.0f)
        //    {
        //        agent.isStopped = true;
        //        animator.SetBool("running", false);
        //        animator.SetBool("attack", true);
        //    }
        //    if (agent.remainingDistance > 5.0f)
        //    {
        //        animator.SetBool("attck", false);
        //        animator.SetBool("running", true);
        //    }
        //}
        //print(agent.velocity.sqrMagnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            target = other.gameObject;
            targetFound = true;
            agent.destination = target.transform.position;
            agent.isStopped = false;
            animator.SetBool("running", true);
        }
    }
}
