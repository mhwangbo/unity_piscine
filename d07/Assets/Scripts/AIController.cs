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


    public GameObject[] explosionParticles;
    private bool shooting;
    private Coroutine shootCoroutine;

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
                shootCoroutine = StartCoroutine(ShootTarget());
            }
        }
    }

    private IEnumerator ShootTarget()
    {
        while (true)
        {
            // randomly rotate and shoot
            float randX = Random.Range(-0.2f, 0.2f);
            float randY = Random.Range(-0.2f, 0.2f);
            Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
            fwd.x += randX;
            fwd.y += randY;
            RaycastHit hit;
            if (Physics.Raycast(cannon.transform.position, fwd, out hit, 50.0f))
            {
                int type = Random.Range(0, 2);
                StartCoroutine(Explosion(explosionParticles[type], hit.point));
                if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
                {
                    // decrease target's health
                    print("Player health Decreased");
                }
                yield return new WaitForSeconds(type == 0 ? 2.0f : 1.0f);
                print("RandX: " + randX + ";  RandY: " + randY + ";  Type: " + type);
            }
        }
    }

    private IEnumerator Explosion(GameObject particle, Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(particle, pos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(tmp);
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
            agent.destination = goal.position;
            if (shooting)
            {
                shooting = false;
                StopCoroutine(shootCoroutine);
            }
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
                locked = false;
                if (shooting)
                {
                    shooting = false;
                    StopCoroutine(shootCoroutine);
                }
            }
        }
    }

}
