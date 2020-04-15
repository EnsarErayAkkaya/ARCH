using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    private Transform player;
    private Vector2 direction;

    public GameObject particle;
    public float lifeTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = player.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        Destroy(gameObject,lifeTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag  == "Player" )
        {
            other.gameObject.GetComponent<Player>().GetDamage(damage);
            DestroyProjectile();
        }
    }
    public void DestroyProjectile()
    {
        var part = Instantiate(particle,transform.position,Quaternion.identity);
        part.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
