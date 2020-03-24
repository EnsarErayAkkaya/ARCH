using UnityEngine;
using Pathfinding;

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
    public bool canShoot,canRotate,dontDieFromCollision,isSloved,dontGetDamage;
    public GameObject projectile;
    public RoomController room;
    void Start()
    {
        gameObject.AddComponent<EnemyUI>();
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>().transform;
        selfRotationSpeed.z = Random.Range(-60,60);
        if(GetComponent<AIDestinationSetter>() != null)
        {
            GetComponent<AIDestinationSetter>().target = target;
        }
        timeBetweenShots = startTimeBetweenShots;
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
   

   void OnCollisionEnter2D(Collision2D other)
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
        if(dontGetDamage == true)
            return;
		currentHealth -= damage;
        if(currentHealth <=0)
        {
            FindObjectOfType<Player>().AddEnemyKilled();
            DestroyEnemy();
        }
        GetComponent<EnemyUI>().UpdateHealthBar(currentHealth,maxHealth);
	}
    public Transform Get_Target()
    {
        return target;
    }

}
