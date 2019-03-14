using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform goal;
    public GameObject cannon;
    public float hp;

    private NavMeshAgent agent;
    private GameObject target;
    private bool locked;
    private Vector3 cannonOrig;
    private bool inRange;

    private bool player;
    private bool enemy;

    private int enemyState;

    private bool killed;


    public GameObject[] explosionParticles;
    public GameObject tankExplosion;
    private bool shooting;
    private Coroutine shootCoroutine;
    private bool enemyKilled;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        cannonOrig = cannon.transform.eulerAngles;
    }

    private void Update()
    {
        if(inRange)
        {
            cannon.transform.LookAt(target.transform.position);
            cannon.transform.eulerAngles = new Vector3(cannonOrig.x, cannon.transform.eulerAngles.y, 0.0f);
            RayCastFocus();
        }
        if(locked)
        {
            agent.isStopped = true;
            if (!shooting)
            {
                shooting = true;
                shootCoroutine = StartCoroutine(ShootTarget());
            }
        }
        if (hp <= 0 && !killed)
            StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        killed = true;
        GameObject tmp = (GameObject)Instantiate(explosionParticles[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Destroy(tmp);
        Destroy(this.gameObject);
    }

    private IEnumerator ShootTarget()
    {
            TankController tankController = null;
            AIController aIController = null;
            if (player)
                tankController = target.GetComponent<TankController>();
            else if (enemy)
                aIController = target.GetComponent<AIController>();
            
        while (true)
        {
            // randomly rotate and shoot
            float randY = Random.Range(-0.2f, 0.2f);
            Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
            fwd.y += randY;
            RaycastHit hit;
            if (Physics.Raycast(cannon.transform.position, fwd, out hit, 50.0f))
            {
                int type = Random.Range(0, 2);
                StartCoroutine(Explosion(explosionParticles[type], hit.point));
                if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
                {
                    if (player)
                        enemyState = tankController.HPDecrease(type == 0 ? 5.0f : 0.5f);
                    else if (enemy)
                        enemyState = aIController.HPDecrease(type == 0 ? 5.0f : 0.5f);
                }
                yield return new WaitForSeconds(type == 0 ? 2.0f : 1.0f);
            }
            if (enemyState == 1)
            {
                enemyKilled = true;
                yield return new WaitForSeconds(1.0f);
                ResetVal();
            }
        }
    }

    public int HPDecrease(float damage)
    {
        hp -= damage;
        if (hp > 0)
            return (0);
        else
            return (1);
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
            inRange = true;
            if (!target)
                target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Enemy")
        {
            ResetVal();
        }
    }

    private void RayCastFocus()
    {
        Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(cannon.transform.position, fwd, out hit, 50.0f))
        {
            if (hit.transform.gameObject != transform.gameObject && (hit.transform.tag == "Player" || hit.transform.tag == "Enemy"))
            {
                locked = true;
                if (hit.transform.tag == "Player")
                    player = true;
                else
                    enemy = true;
            }
            else
            {
                if (!enemyKilled)
                    agent.destination = target.transform.position;
                else
                    agent.destination = goal.position;
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

    private void ResetVal()
    {
        inRange = false;
        target = null;
        locked = false;
        agent.isStopped = false;
        player = false;
        enemy = false;
        enemyKilled = false;
        agent.destination = goal.position;
        if (shooting)
        {
            shooting = false;
            StopCoroutine(shootCoroutine);
        }
    }

}
