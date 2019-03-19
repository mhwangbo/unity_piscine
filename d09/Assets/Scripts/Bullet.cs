using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 target;
    private float time;
    public GameObject particle;
    private bool destroyStart;
    // for area damage
    public float range;
    public float damage;

    private void Update()
    {
        time += Time.deltaTime;
        float step = 20.0f * Time.deltaTime;
        Vector3 targetDir = Vector3.MoveTowards(transform.position, target, step);
        transform.position = targetDir;
        if (time >= 10.0f)
            Destroy(this.gameObject);
        //if (Vector3.Distance(transform.position, target) <= 0.1f)
            //Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Boundary")
        {
            if (!destroyStart)
                StartCoroutine(Collide(collision.transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Boundary")
        {
            if (!destroyStart)
                StartCoroutine(Collide(other.transform.position));
        }
    }

    private IEnumerator Collide(Vector3 pos)
    {
        destroyStart = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        GameObject par = Instantiate(particle);
        par.transform.position = target;
        yield return new WaitForSeconds(0.5f);
        if (range > 0)
        {
            SphereCollider sphere = gameObject.AddComponent<SphereCollider>();
            sphere.radius = range / 2;
            sphere.isTrigger = true;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(par);
        Destroy(this.gameObject);
    }
}
