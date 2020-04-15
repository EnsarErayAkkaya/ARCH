using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
	[SerializeField] int maxHealth;
    private Player target;
    private Vector3 selfRotationSpeed;
    [SerializeField] float stoppingDistance,startTimeBetweenShots,damage,projectileExtraDistanceToCenter;
    float timeBetweenShots;
    [SerializeField] GameObject enemyParticle,projectile;
    public GameObject spawnParticle;
    [SerializeField] bool canShoot,dontDieFromCollision;
    public bool isSloved,dontGetDamage;
    SurvivalGameManager gameManager;
    [SerializeField] EnemyUI enemyUI;
    void Start()
    {
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>();
        selfRotationSpeed.z = Random.Range(-60,60);
        if(GetComponent<AIDestinationSetter>() != null)
        {
            GetComponent<AIDestinationSetter>().target = target.transform;
        }
        timeBetweenShots = startTimeBetweenShots;
        gameManager = FindObjectOfType<SurvivalGameManager>();
    }

    void Update()
    {
        Shoot();
    }
   

   void OnCollisionEnter2D(Collision2D other)
   {
        if(other.gameObject.CompareTag("Player") && !dontDieFromCollision)
        {
            other.gameObject.GetComponent<Player>().GetDamage((int)damage);
            FindObjectOfType<SurvivalGameManager>().LoseScore();
            
            DestroyEnemy();
       }
   }
  
    void Shoot()
    {
        if (canShoot)
        {
            if(Vector2.Distance(transform.position, target.transform.position) < stoppingDistance)
            {
                if(timeBetweenShots <= 0)
                {
                    Vector2 dir = (target.transform.position - transform.position).normalized * projectileExtraDistanceToCenter;
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
        FindObjectOfType<SurvivalEnemyManager>().OnEnemyKilled(this.gameObject);
        if(GetComponent<Enemy_P>() != null)
        {
            GetComponent<Enemy_P>().OnHealthZero();
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
            target.AddEnemyKilled();
            DestroyEnemy();
        }
        enemyUI.UpdateHealthBar(currentHealth,maxHealth);
	}
}
