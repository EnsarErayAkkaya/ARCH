using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorWall : MonoBehaviour
{
    public bool right,left,bottom,top;
    public bool damagePassanger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile"))
		{
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Reflect_Projectile( other.gameObject.GetComponent<Rigidbody2D>().velocity );
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                other.gameObject.GetComponent<Projectile>().Bomb();
            }
            //other.gameObject.GetComponent<Projectile>().DestroyProjectile();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("Player"))
		{
            //give DamageTo Passangers
            if(damagePassanger == true)
            {
                FindObjectOfType<PassangerManager>().GetDamage();
            }            ///Stop the player
            other.gameObject.GetComponent<Player_Shoot>().canRecoil = false;
        }
        
    }
    

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
		{
            ///Stop the player
            other.gameObject.GetComponent<Player_Shoot>().canRecoil = true;
        }
    }

   
    Vector2 Reflect_Projectile(Vector2 velocity)
    {
        if( right == true )
        {
            velocity.x *= -1;
        } 
        else if( left == true )
        {
            velocity.x *= -1;
        }
        else if( bottom == true )
        {
            velocity.y *= -1;
        }
        else if( top == true )
        {
            velocity.y *= -1;
        }
        return velocity;
    }
}
