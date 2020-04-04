using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
	[SerializeField] int maxHealth;
    private Transform target;
    private Vector3 selfRotationSpeed;
    [SerializeField] float stoppingDistance,startTimeBetweenShots,damage,projectileExtraDistanceToCenter;
    float timeBetweenShots;
    [SerializeField] GameObject enemyParticle,projectile;
    public GameObject spawnParticle;
    [SerializeField] bool canShoot,canRotate,dontDieFromCollision;
    public bool isSloved,dontGetDamage;
    public RoomController room;
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
    }

    void Update()
    {
        if(canRotate)
        {
            RotateSelf();

        }
        Shoot();
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
   /* void OnDrawGizmos()
   {
       Gizmos.DrawWireSphere(transform.position,stoppingDistance);
   } */
    void RotateSelf()
    {
        transform.Rotate(selfRotationSpeed * Time.deltaTime);
    }
  
    void Shoot()
    {
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            if(canShoot)
            {
                if(timeBetweenShots <= 0)
                {
                    Vector2 dir = (target.position - transform.position).normalized * projectileExtraDistanceToCenter;
                    Vector2 pos = new Vector2(transform.position.x + dir.x,transform.position.y+ dir.y);
                    Instantiate(projectile, pos, Quaternion.identity);

                    timeBetweenShots = startTimeBetweenShots;
                }
                else
                {
                    timeBetweenShots -= Time.deltaTime;
                }
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
