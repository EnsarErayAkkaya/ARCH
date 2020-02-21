using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
	public int maxHealth;
    private Transform target;
    private Vector3 selfRotationSpeed;
    public float damage;
    public float stoppingDistance,retreatDistance;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public GameObject enemyParticle,spawnParticle;
    public bool canShoot,canRotate,dontDieFromCollision,isSloved;
    public GameObject projectile;
    public RoomController room;

    //private Rigidbody2D rb;
    //float lastVelocityX;

   //public bool collidingWithWall;
    //public float friendRadius;

    void Start()
    {
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>().transform;
        selfRotationSpeed.z = Random.Range(-60,60);
        if(GetComponent<AIDestinationSetter>() != null)
        {
            GetComponent<AIDestinationSetter>().target = target;
        }
        timeBetweenShots = startTimeBetweenShots;
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(canRotate)
        {
            RotateSelf();

        }
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance )
        {
            Shoot();
        }
    }
   

   void OnTriggerEnter2D(Collider2D other)
   {
       if(!dontDieFromCollision)
       {
           if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().GetDamage((int)damage);
                if( FindObjectOfType<SurvivalGameManager>() != null )
                {
                    FindObjectOfType<SurvivalGameManager>().LoseScore();
                }
                DestroyEnemy();
            }
       }
       
   }
    void RotateSelf()
    {
        transform.Rotate(selfRotationSpeed * Time.deltaTime);
    }
  
    void Shoot()
    {
        if(canShoot)
        {
            if(timeBetweenShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }
 
    public void DestroyEnemy()
    {
        if(FindObjectOfType<SurvivalEnemyManager>() != null)
        {
            FindObjectOfType<SurvivalEnemyManager>().OnEnemyKilled(this.gameObject);
        }
        else
        {
            room.RemoveEnemyWhenDied(gameObject);
        }
        var particle = Instantiate(enemyParticle,transform.position,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

    public void GetDamage(int damage)
	{
		currentHealth -= damage;
        if(currentHealth <=0)
        {
            FindObjectOfType<Player>().AddEnemyKilled();
            DestroyEnemy();
        }
	}
    public Transform Get_Target()
    {
        return target;
    }

}
