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

    public float lifeTime;
    public bool spawnEnemy;

    void Start()
    {
        DetectLifeTime();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
		{
            
            //Debug.Log("düşman");   
            if(FindObjectOfType<PermanentPowerUpController>().freezingShot)
            {
                FreezeEnemy(other.gameObject);
            }

            life--;
            if(isBomb)
            {
                Bomb();
            }
            else
            {
                GiveDamage(other.gameObject);
            }
        }
        if(other.gameObject.CompareTag("Nest"))
		{
            life--;
            Bomb();
            other.gameObject.GetComponent<Enemy_Spawn>().GetDamage(1);
            DestroyProjectile();
        }
        
    }
    

    public void GiveDamage(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetDamage(damage);

        DestroyProjectile();
    }

    public void Bomb()
    {
        if(isBomb)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            if( gos.Length > 0 )
            {
                foreach (var col in gos)
                {
                    if( radius >= Vector2.Distance(col.transform.position, transform.position) )
                    {
                        col.GetComponent<Enemy>().GetDamage(damage);
                    }
                }
            }
        }
        DestroyProjectile();

    }
    void DetectLifeTime()
    {
        Destroy(gameObject,lifeTime);
    }
    public void DestroyProjectile()
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
    public void FreezeEnemy(GameObject enemy)
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
