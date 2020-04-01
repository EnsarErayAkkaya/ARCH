using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throver_Wall : MonoBehaviour
{
    public GameObject center;
    public float throvingPower;
    
    public bool damagePassanger,throwWalls;

    void  OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
            //give DamageTo Passangers
            if(damagePassanger == true)
            {
                FindObjectOfType<PassangerManager>().GetDamage();
            }
            other.gameObject.GetComponent<Player_Shoot>().recoiledVector = ThrowPlayer(other.gameObject);
            other.gameObject.GetComponent<Player_Shoot>().recoilCall = true;
        }
        if(other.gameObject.CompareTag("Wall"))
		{
            if(throwWalls == true)
            {
                other.gameObject.GetComponent<AddForceToWall>().AddForce = true;    
            }  
        }
        if(other.gameObject.CompareTag("Projectile"))
		{
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                other.gameObject.GetComponent<Projectile>().life = 0;
            other.gameObject.GetComponent<Projectile>().CollisionInterract(gameObject);
            }
            if(other.gameObject.GetComponent<Enemy_Projectile>() != null)
            {
                other.gameObject.GetComponent<Enemy_Projectile>().DestroyProjectile();
            }
        }
	}

    public Vector2 ThrowPlayer(GameObject other)
    {
        return (center.transform.position - other.GetComponent<Transform>().position).normalized * throvingPower;
    }
}
