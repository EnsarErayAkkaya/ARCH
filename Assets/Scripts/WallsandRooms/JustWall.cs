using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustWall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("Player"))
		{
            ///Stop the player
            other.gameObject.GetComponent<Player_Shoot>().canRecoil = false;
        }
        if(other.gameObject.CompareTag("Projectile"))
		{
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                other.gameObject.GetComponent<Projectile>().life = 0;
                other.gameObject.GetComponent<Projectile>().Bomb();
                other.gameObject.GetComponent<Projectile>().DestroyProjectile();
            }
            if(other.gameObject.GetComponent<Enemy_Projectile>() != null)
            {
                other.gameObject.GetComponent<Enemy_Projectile>().DestroyProjectile();
            }
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
}
