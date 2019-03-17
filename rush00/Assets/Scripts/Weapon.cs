using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite equipped;
    [SerializeField] private Sprite unequipped;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Sprite bulletImage;

    // Weapon stats
    [SerializeField] private bool fireArms;
    [SerializeField] private float range;
    [SerializeField] private int ammo;
    [SerializeField] private float shotSpeed;

    [SerializeField] private string type;

    private bool hot;

    public bool FireArms { get { return fireArms; } }
    public float Range { get { return range; } }
    public int Ammo { get { return ammo; } }
    public float ShotSpeed { get { return shotSpeed; } }
    public Sprite Equipped { get { return equipped; }}

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!fireArms)
            range = 0.5F;
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = equipped;
    }

    public void Shot()
    {
        if (!fireArms || gameObject.layer == 9)
            ammo = 1;
        if (!hot && ammo > 0)
        {
            GameObject shot = Instantiate(bullet);
            shot.layer = gameObject.layer;
            shot.transform.position = transform.position;
            shot.transform.rotation = transform.rotation;
            shot.GetComponent<SpriteRenderer>().sprite = bulletImage;
            shot.AddComponent(typeof(BoxCollider2D));
            shot.GetComponent<Bullet>().range = range;
            StartCoroutine(Cool());
            ammo--;
        }
    }

    private IEnumerator Cool()
    {
        hot = true;
        yield return new WaitForSeconds(shotSpeed);
        hot = false;
    }

    public void Dropped()
    {
        StartCoroutine(Drop());
        spriteRenderer.sprite = unequipped;
    }

    private IEnumerator Drop()
    {
        spriteRenderer.sprite = unequipped;
        GameObject thrown = Instantiate(gameObject);
        spriteRenderer.enabled = false;
        thrown.transform.position = transform.position;
        thrown.transform.rotation = transform.rotation;
        thrown.layer = 0;
        BoxCollider2D box = thrown.GetComponent<BoxCollider2D>();
        box.enabled = true;
        while (Vector3.Distance(transform.position, thrown.transform.position) < 3.0f)
        {
            thrown.transform.Translate(Vector3.up * 10.0f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
