using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyY_Controller : MonoBehaviour
{
    public float detectDistance;
    private Transform target;
    //public Animator animator;
    public float attackForce;
    private bool attack,attackStarted;
    private Vector3 attackPos;
    public Animator animator;
    Rigidbody2D rb;
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        rb =  GetComponent<Rigidbody2D>();        
    }
   
    void Update()
    {
        Look_Player();
        if( attackStarted == false )
        {
            //Debug.Log("here");
            if(Vector2.Distance(target.position,transform.position) < detectDistance)
            {
                //Debug.Log(Vector2.Distance(target.position,transform.position));
                attackStarted = true;
                animator.SetBool("Attacking",true);
                InvokeRepeating("Attack",2,2);
            }
        }
    }
  
    void FixedUpdate()
    {
        if(attack)
        {
            rb.AddForce(attackPos * attackForce,ForceMode2D.Impulse);
            //animator.SetBool("Attacking",false);
            attack = false;
        }
    }
    void Attack()
    {
        var dir = target.position - transform.position;
        attackPos = dir.normalized;
        attack = true;

    }   
    void Look_Player()
    {
        var dir = target.position - transform.position;
        var angle =  Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
    }
}
