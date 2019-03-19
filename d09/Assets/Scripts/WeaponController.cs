using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponController : MonoBehaviour
{
    // Stats
    [SerializeField] private bool areaDamage;
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;

    // Visual and Audio
    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private GameObject shotPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Material bulletTrail;
    [SerializeField] private Animator animator;

    // getter
    public bool AreaDamage { get { return areaDamage; } }
    public float Range { get { return range; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public float Damage { get { return damage; } }
    public AudioSource ShotAudio { get { return shotAudio; } }
    public GameObject ShotPoint { get { return shotPoint; } }
    public GameObject Bullet { get { return bullet; } }

    // For functions
    private Transform gunPoint;
    public GameObject cursor;
    [SerializeField] private bool ableToShoot;
    private PlayerController playerController;

    private void Start()
    {
        gunPoint = transform.Find("Cannon Point");
        ableToShoot = true;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ableToShoot && !playerController.IsKilled)
        {
            animator.SetTrigger("Shot");
        }

    }

    IEnumerator Shoot()
    {
        ableToShoot = false;
        shotAudio.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(cursor.transform.position), out hit, 400))
        {
            GameObject shot = Instantiate(bullet);
            Bullet shotScript = shot.GetComponent<Bullet>();
            if (areaDamage)
                shotScript.range = range;
            shotScript.target = hit.point;
            shotScript.damage = damage;
            shot.GetComponent<TrailRenderer>().material = bulletTrail;
            shot.transform.position = shotPoint.transform.position;
            shot.transform.LookAt(hit.point);
        }
        yield return new WaitForSeconds(AttackSpeed);
        ableToShoot = true;
    }
}