using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int life;
    public float radius;
    public bool isBomb;
    public GameObject normalParticle,middleParticle,powerfulParticle,freezingParticle;
    public ProjectileType type;
    public int damage;

    public float lifeTime, powerfulProjectileExplosionForce;
    public bool spawnEnemy;

    void Start()
    {
        DetectLifeTime();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
		{
            if(FindObjectOfType<PermanentPowerUpController>().freezingShot)
            {
                FreezeEnemy(other.gameObject);
            }

            CollisionInterract(other.gameObject);
        }
        if(other.gameObject.CompareTag("Nest"))
		{
            life--;
            if(isBomb)
            {
                Bomb();
            }
            other.gameObject.GetComponent<Enemy_Spawn>().GetDamage(1);
            DestroyProjectile();
        }
        
    }

    public void CollisionInterract(GameObject other)
    {
        life--;
        if(isBomb)
        {
            Bomb();
        }
        else
        {
            GiveDamage(other);
        }
    }    

     void GiveDamage(GameObject enemy)
    {
        if( enemy.GetComponent<Enemy>() != null )
            enemy.GetComponent<Enemy>().GetDamage(damage);

        DestroyProjectile();
    }
    ///<summary>
    ///Etrafındaki objeleri iter. Player ve enemylere hasar verir. camları da bir seviye parçalar
    ///</summary>
     void Bomb()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, 8))
        {
            if(col.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 Direction = col.transform.position - transform.position;
                col.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * powerfulProjectileExplosionForce,ForceMode2D.Impulse );
                if(col.GetComponent<Player>() != null)
                {
                    //Yakında patlarsa sende hasar yersin.
                    col.GetComponent<Player>().GetDamage( damage );
                }
                else if(col.GetComponent<Enemy>() != null)
                {
                    GiveDamage(col.gameObject);
                }
            }
        }
        DestroyProjectile();

    }
    void DetectLifeTime()
    {
        Destroy(gameObject,lifeTime);
    }
     void DestroyProjectile()
    {
        if(life<1)
        {
            if(type == ProjectileType.Normal)
            {
                var particle = Instantiate(normalParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
            else if(type == ProjectileType.Middle)
            {
                var particle = Instantiate(middleParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
            else if(type == ProjectileType.Powerful)
            {
                var particle = Instantiate(powerfulParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
        }
        if(spawnEnemy)
        {
            FindObjectOfType<Enemy_Controller>().SpawnEnemy(0,transform.position);
        }
    }
     void FreezeEnemy(GameObject enemy)
    {
        if(enemy.GetComponent<AIPath>() != null)
        {
            if( enemy.GetComponent<Enemy>().isSloved == false)
            {
                enemy.GetComponent<AIPath>().maxSpeed -= 1.2f;
                var particle = Instantiate(freezingParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                enemy.GetComponent<Enemy>().isSloved = true;
            }
        }
        else
        {
            Debug.Log("Yavaşlatılamadı");
        }
        
        
    }
}
public enum ProjectileType
{
    Normal,Middle,Powerful
}
