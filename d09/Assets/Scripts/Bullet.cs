using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    public Vector3 target;
    private float time;
    public GameObject particle;

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
        StartCoroutine(Collide(collision.transform.position));
    }

    private IEnumerator Collide(Vector3 pos)
    {
        GameObject par = Instantiate(particle);
        par.transform.position = target;
        yield return new WaitForSeconds(3.0f);
        Destroy(par);
        Destroy(this.gameObject);
    }
}
