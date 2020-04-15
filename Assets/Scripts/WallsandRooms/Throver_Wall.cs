using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throver_Wall : MonoBehaviour
{
    public GameObject center;
    public float throvingPower;
    
    public bool damagePassanger,throwWalls;

    [SerializeField]Player_Shoot player_Shoot;
    void  OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
            player_Shoot.recoiledVector = ThrowPlayer(other.gameObject);
            player_Shoot.recoilCall = true;
        }
        if(other.gameObject.CompareTag("Wall"))
		{
            if(throwWalls == true)
            {
                other.gameObject.GetComponent<AddForceToWall>().StartForce();  
            }  
        }
        if(other.gameObject.CompareTag("Projectile"))
		{
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                Projectile p = other.gameObject.GetComponent<Projectile>();
                p.life = 0;
                p.CollisionInterract(gameObject);
            }
            if(other.gameObject.GetComponent<Enemy_Projectile>() != null)
            {
                other.gameObject.GetComponent<Enemy_Projectile>().DestroyProjectile();
            }
        }
	}

    public Vector2 ThrowPlayer(GameObject other)
    {
        return (center.transform.position - other.transform.position).normalized * throvingPower;
    }
}
