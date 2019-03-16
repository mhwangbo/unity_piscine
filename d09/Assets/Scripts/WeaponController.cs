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
    [SerializeField] private GameObject trail;
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
    public GameObject Trail { get { return trail; } }

    public MeshRenderer Mesh { get { return mesh; } set { mesh.enabled = value; } }
    public SkinnedMeshRenderer SkinnedMesh { get { return skinnedMesh; } set { skinnedMesh.enabled = value; } }

    public void TriggerAnimation()
    {
        animator.SetTrigger("shot");
    }
}

//[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]
//public class WeaponData : ScriptableObject
//{
//    // Stats
//    [SerializeField] private bool areaDamage;
//    [SerializeField] private float range;
//    [SerializeField] private float attackSpeed;
//    [SerializeField] private float damage;

//    // Visual and Audio
//    [SerializeField] private AudioSource shotAudio;
//    [SerializeField] private GameObject shotPoint;
//    [SerializeField] private GameObject trail;
//    [SerializeField] private MeshRenderer mesh;
//    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

//    // getter
//    public bool AreaDamage { get { return areaDamage; } }
//    public float Range { get { return range; } }
//    public float AttackSpeed { get { return attackSpeed; } }
//    public float Damage { get { return damage; } }
//    public AudioSource ShotAudio { get { return shotAudio; } }
//    public GameObject ShotPoint { get { return shotPoint; } }
//    public GameObject Trail { get { return trail; } }

//    public MeshRenderer Mesh { get { return mesh; } set { mesh.enabled = value; } }
//    public SkinnedMeshRenderer SkinnedMesh { get { return skinnedMesh; } set { skinnedMesh.enabled = value; } }
//}