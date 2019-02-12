using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footman : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector3 target;

    public Animator anim;
    public bool selected = false;
    private bool isRight = true;
    private float dirX;
    private float dirY;

    void Start()
    {
        target = transform.position;
        anim = GetComponent<Animator>();
    }

    public void WhenSelected()
    {
        if (selected)
            InvokeRepeating("Move", 0, 0.1f);
        else if (!selected)
            CancelInvoke();
    }

    void Move()
    {
           if (Input.GetMouseButtonDown(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                dirX = target.x - transform.position.x;
                dirY = target.y - transform.position.y;
                anim.SetBool("walk", true);
                anim.SetFloat("x", target.x - transform.position.x);
                anim.SetFloat("y", target.y - transform.position.y);
                flip();
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
