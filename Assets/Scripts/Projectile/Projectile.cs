using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int life;
    public float radius = 8;
    public bool isBomb;
    public ProjectileType type;
    public int damage;

    public bool start;
    ProjectileManager manager;
    Rigidbody2D rb;
    public Vector2 shootDirection;
    public void SetProjectile(Vector2 dir)
    {
        manager = FindObjectOfType<ProjectileManager>();
        DetectLifeTime();
        rb = GetComponent<Rigidbody2D>();
        start = true;
        SetSize();
		shootDirection = dir;   
    }
    void FixedUpdate()
    {
        if(start)
        {
            start = false;
            rb.velocity = shootDirection * manager.MiddleShootSpeed;///Speed
        }
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
        if(enemy.GetComponent<Enemy>()!= null)
        {
            enemy.GetComponent<Enemy>().GetDamage(damage);
        }
        DestroyProjectile();
    }
    ///<summary>
    ///Etrafındaki objeleri iter. Player ve enemylere hasar verir. camları da bir seviye parçalar
    ///</summary>
     void Bomb()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if(col.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 Direction = col.transform.position - transform.position;
           
                col.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * manager.powerfulProjectileExplosionForce,ForceMode2D.Impulse );
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
        Destroy(gameObject,manager.lifeTime);
    }
    void DestroyProjectile()
    {
        if(life<1)
        {
            if(type == ProjectileType.Normal)
            {
                var particle = Instantiate(manager.normalParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
            else if(type == ProjectileType.Middle)
            {
                var particle = Instantiate(manager.middleParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
            else if(type == ProjectileType.Powerful)
            {
                var particle = Instantiate(manager.powerfulParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(gameObject);
            }
        }
    }
    void SetSize()
    {
            if(type == ProjectileType.Normal)
            {
                transform.localScale *= manager.NormalShootSize; ///Size
            }
            else if(type == ProjectileType.Middle)
            {
                transform.localScale *= manager.MiddleShootSize; ///Size
            }
            else if(type == ProjectileType.Powerful)
            {
                transform.localScale *= manager.PowerfulShootSize; ///Size
            }
        
        
    }
    void FreezeEnemy(GameObject enemy)
    {
        if(enemy.GetComponent<AIPath>() != null)
        {
            if( enemy.GetComponent<Enemy>().isSloved == false)
            {
                enemy.GetComponent<AIPath>().maxSpeed -= 1.8f;
                var particle = Instantiate(manager.freezingParticle,transform.position,Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                enemy.GetComponent<Enemy>().isSloved = true;
            }
        }        
    }
}
public enum ProjectileType
{
    Normal,Middle,Powerful
}
