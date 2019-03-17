using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        GameObject w = weapons[Random.Range(0, weapons.Length)];
        GameObject weapon = Instantiate(w);
        weapon.transform.name = w.transform.name;
        weapon.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
