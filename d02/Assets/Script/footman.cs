using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footman : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector3 target;

    public Animator anim;
    public bool isSelected = false;
    private bool isRight = true;
    private float dirX;
    private float dirY;

    public footmanController fmController;
    private AudioSource walkingSound;

    void Start()
    {
        target = transform.position;
        anim = GetComponent<Animator>();
        walkingSound = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        if (!isSelected)
        {
            enabled = true;
            isSelected = true;
            if (Input.GetKey(KeyCode.LeftControl))
                fmController.footmanList.Add(this);
            else
            {
                foreach (footman fm in fmController.footmanList)
                {
                    if (fm.isSelected)
                    {
                        fm.isSelected = false;
                    }
                }
                fmController.footmanList.Clear();
                fmController.footmanList.Add(this);
            }

        }
    }

    void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (!hit || (hit.collider.gameObject.transform.tag != "footman"))
                {
                    walkingSound.Play();
                    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = transform.position.z;
                    dirX = target.x - transform.position.x;
                    dirY = target.y - transform.position.y;
                    anim.SetBool("walk", true);
                    anim.SetFloat("x", dirX);
                    anim.SetFloat("y", dirY);
                    flip();
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            anim.SetBool("walk", false);
        }
    }

    void flip()
    {
        int sign = 1;
        if (isRight && dirX <= 0)
        {
            sign = -1;
            isRight = false;
        }
        else if (!isRight && dirX > 0)
        {
            sign = -1;
            isRight = true;
        }
        Vector3 temp = anim.transform.localScale;
        temp.x *= sign;
        anim.transform.localScale = temp;
    }
}
