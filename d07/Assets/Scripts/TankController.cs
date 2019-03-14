using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    // tank movement
    private float speedM = 5.0f;
    private float speedR = 60.0f;
    private float boostLimit = 50.0f;

    // cannon movement
    private float yaw = 0.0f;
    private float speedH = 20.0f;
    public GameObject cannon;
    private Vector3 cannonOriginal;

    // shoot 0 = missile, 1 = machineGun
    public GameObject[] explosionParticles;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private float[] powers = new float[2];
    private float[] lengths = new float[2];
    private Coroutine shootCoroutine;

    // tank status
    public float hp;
    private int missileLimit = 50;
    public bool killed;

    private void Start()
    {
        cannonOriginal = cannon.transform.eulerAngles;
        powers[0] = 10.0f;
        powers[1] = 1.0f;
        lengths[0] = 60.0f;
        lengths[1] = 30.0f;
    }

    void Update()
    {
        TankMovement();
        Shoot();

        // For easy testing
        if (Input.GetKey("r"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (!killed && hp <= 0)
            StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        killed = true;
        GameObject tmp = (GameObject)Instantiate(explosionParticles[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Destroy(tmp);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TankMovement()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        cannon.transform.eulerAngles = new Vector3(cannonOriginal.x, yaw, 0.0f);
        if (Input.GetKey("w"))
            transform.Translate(Vector3.forward * Time.deltaTime * speedM);
        if (Input.GetKey("s"))
            transform.Translate(Vector3.back * Time.deltaTime * speedM);
        if (Input.GetKey("a"))
            transform.Rotate(0, Input.GetAxis("Horizontal") * speedR * Time.deltaTime, 0);
        if (Input.GetKey("d"))
            transform.Rotate(0, Input.GetAxis("Horizontal") * speedR * Time.deltaTime, 0);
        if (boostLimit > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            speedM = 10.0f;
            boostLimit -= 0.2f;
        }
        else
        {
            speedM = 5.0f;
            if (boostLimit < 50f)
                boostLimit += 0.1f;
        }
    }

    private void Shoot()
    {
        // 0: Left click, 1: Right click

        if (Input.GetMouseButtonDown(1))
            shootCoroutine = StartCoroutine(MachineGunShooting());
        if (missileLimit > 0 && Input.GetMouseButtonDown(0))
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            ShootRaycast(0);
            missileLimit -= 1;
        }
        if (Input.GetMouseButtonUp(1))
            StopCoroutine(shootCoroutine);
    }

    private IEnumerator MachineGunShooting()
    {
        while (true)
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            ShootRaycast(1);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void ShootRaycast(int type)
    {
        Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(cannon.transform.position, fwd, out hit, lengths[type]))
        {
            StartCoroutine(Explosion(explosionParticles[type], hit.point));
            if (hit.transform.tag == "Enemy")
            {
                AIController aIController = null;
                aIController = hit.transform.gameObject.GetComponent<AIController>();
                aIController.HPDecrease(type == 0 ? 2.0f : 1.0f);
            }
        }
    }

    private IEnumerator Explosion(GameObject particle, Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(particle, pos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(tmp);
    }

    public int HPDecrease(float damage)
    {
        hp -= damage;
        if (hp > 0)
            return (0);
        else
        {
            Instantiate(explosionParticles[0], transform.position, Quaternion.identity);
            return (1);
        }
    }
}
