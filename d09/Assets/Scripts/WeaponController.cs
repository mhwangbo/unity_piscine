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
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private Animator animator;

    // getter
    public bool AreaDamage { get { return areaDamage; } }
    public float Range { get { return range; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public float Damage { get { return damage; } }
    public AudioSource ShotAudio { get { return shotAudio; } }
    public GameObject ShotPoint { get { return shotPoint; } }
    public GameObject Bullet { get { return bullet; } }

    public MeshRenderer Mesh { get { return mesh; } set { mesh.enabled = value; } }
    public SkinnedMeshRenderer SkinnedMesh { get { return skinnedMesh; } set { skinnedMesh.enabled = value; } }

    // For functions
    private Transform gunPoint;
    public GameObject cursor;
    private bool ableToShoot;

    private void Start()
    {
        gunPoint = transform.Find("Cannon Point");
        ableToShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ableToShoot)
        {
            animator.SetTrigger("Shot");
        }

    }

    IEnumerator Shoot()
    {
        ableToShoot = false;
        shotAudio.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(cursor.transform.position), out hit, 100))
        {
            GameObject shot = Instantiate(bullet);
            shot.GetComponent<TestBullet>().target = hit.point;
            shot.GetComponent<TrailRenderer>().material = bulletTrail;
            shot.transform.position = shotPoint.transform.position;
            //shot.transform.localPosition = Vector3.zero;
            shot.transform.LookAt(hit.point);
        }
        yield return new WaitForSeconds(AttackSpeed);
        ableToShoot = true;
    }
}