using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explosionForce;
    public int damage;
    Animator bombAnimator;
    void Start()
    {
        bombAnimator = GetComponent<Animator>();
    }

    public void StartExplosion()
    {
        StartCoroutine( ExplodeRoutine() );        
    }
    IEnumerator ExplodeRoutine()
    {
        
        yield return new WaitForSeconds(1.8f);
        bombAnimator.SetBool("explode",true);
        yield return new WaitForSeconds(1.2f);
        FindObjectOfType<Camera_Shake>().Call_Shake(1f,.3f);///Shakes Camera       
        Explode();
    }
    void Explode()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, 20))
        {
            if(col.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 Direction = col.transform.position - transform.position;
                col.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * explosionForce,ForceMode2D.Impulse );
                if(col.GetComponent<Player>() != null)
                {
                    col.GetComponent<Player>().GetDamage( damage );
                }
                else if(col.GetComponent<GlassScript>() != null)
                {
                    col.GetComponent<GlassScript>().GetDamageFromBomb();
                }
                else if(col.GetComponent<Enemy>() != null)
                {
                    col.GetComponent<Enemy>().GetDamage(damage * 2 / 3);
                }
            }
        }
        Destroy(gameObject);
    }
    
}
