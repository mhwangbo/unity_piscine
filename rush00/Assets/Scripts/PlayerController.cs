using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Camera cam;

    // weapon management
    [SerializeField] private GameObject curWeapon;
    public GameObject CurWeapon { get { return CurWeapon; } }

    private Rigidbody2D rb2D;

    private Weapon weaponStat;
    private bool equipWeapon;
    public bool EquipWeapon { get { return equipWeapon; } }
    private bool shot;

    public bool Shot { get { return shot; } set { shot = value; }}

    private bool isKilled;
    public bool IsKilled{ get { return isKilled; }}

    private bool isWon;
    public bool IsWon{ get { return isWon; }}

    //audio
    //public AudioClip aWin;
    public AudioClip aLose;
    public AudioClip aDrop;
    public AudioClip aPickup;

    private void Start()
    {
        cam = Camera.main;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isKilled && !isWon)
        {
            CharacterMovement();
            if (Input.GetMouseButtonDown(0))
            {
                if (equipWeapon)
                {
                    weaponStat.Shot();
                    if (weaponStat.FireArms)
                        StartCoroutine(ShotSound());
                }
            }
            if (equipWeapon && Input.GetMouseButtonDown(1))
            {
                SoundManager.instance.PlaySingle(aDrop);
                weaponStat.Dropped();
                equipWeapon = false;
                curWeapon = null;
            }
        }
        else if (isKilled)
            StartCoroutine(Killed());
        CheckEnemy();
    }

    private void CheckEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            isWon = true;
    }

    private IEnumerator ShotSound()
    {
        shot = true;
        yield return new WaitForSeconds(0.5f);
        shot = false;
    }

    private IEnumerator Killed()
    {
        SoundManager.instance.PlaySingle(aLose);
        transform.Rotate(Vector3.forward * 500f * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        // Destroy(this.gameObject);
    }

    void CharacterMovement()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);

        Vector3 diff = mousePos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        // transform.position += new Vector3(x, y, 0f) * speed * Time.deltaTime;
        // rb2D.AddForce(new Vector2(x, y) * speed);
        // rb2D.MovePosition(transform.position + new Vector3(x, y, 0f) * Time.deltaTime * speed);
        // rb2D.position += new Vector2(x, y) * speed * Time.deltaTime;
        rb2D.velocity = new Vector2(x, y) * speed;

        Vector3 camPos = new Vector3(transform.position.x, transform.position.y, -10f);
        cam.transform.position = camPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            if (!equipWeapon && Input.GetKeyDown("e"))
            {
                SoundManager.instance.PlaySingle(aPickup);
                equipWeapon = true;
                curWeapon = Instantiate(collision.gameObject, transform.Find("Weapon"));
                curWeapon.transform.name = collision.gameObject.transform.name;
                curWeapon.transform.localRotation = Quaternion.identity;
                curWeapon.transform.localPosition = Vector3.zero;
                curWeapon.layer = gameObject.layer;
                curWeapon.GetComponent<BoxCollider2D>().enabled = false;
                weaponStat = curWeapon.GetComponent<Weapon>();
                weaponStat.ChangeSprite();
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
            isKilled = true;
        if (collision.transform.tag == "Car")
            isWon = true;
    }

}
