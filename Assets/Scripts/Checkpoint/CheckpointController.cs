using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    SurvivalGameManager gameManager;
    int lastCollision;
    float collisionTime;
    public GameObject rightCollider,leftCollider,arrow,leftParticle;
    //sadece önce sağdan geçildiğinde kabul et demek
    public bool isOneSided;
    void Start()
    {
        //Random belirle tek taraflı mı değil mi
        if(Random.Range(0,2)>0) isOneSided = true;

        TurnRandom();

        gameManager = FindObjectOfType<SurvivalGameManager>();
        
        if(isOneSided)
        {
            leftParticle.SetActive(false);
            arrow.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isOneSided)
        {
            if(rightCollider.GetComponent<Collider2D>() & other.gameObject.CompareTag("Player") )
            {
                if(lastCollision == 2 && Time.time - collisionTime < 2.5 )
                {
                    OnPlayerPassed();
                }
                //1 sol
                lastCollision = 1;
                collisionTime = Time.time;
            }
            if( rightCollider.GetComponent<Collider2D>() & other.gameObject.CompareTag("Player") )
            {
                if(lastCollision == 1 && Time.time - collisionTime < 2.5 )
                {
                    OnPlayerPassed();
                }
                    //2 sağ
                lastCollision = 2;
                collisionTime = Time.time;
            }
        }
        else
        {
            if( rightCollider.GetComponent<Collider2D>() & other.gameObject.CompareTag("Player") )
            {
                //1 sol
                lastCollision = 1;
                collisionTime = Time.time;
            }
            if( rightCollider.GetComponent<Collider2D>() & other.gameObject.CompareTag("Player") )
            {
                
            }
        }
    }

    public void Collided(int col,float time)
    {
        IsValid(col);
        lastCollision = col;
        collisionTime = time;
        
    }
    void IsValid(int col)
    {
        if(isOneSided == false)
        {
            if( col == 2 && lastCollision == 1 && Time.time - collisionTime < 2.5 )
            {
                OnPlayerPassed();
            }
            else if( col == 1 && lastCollision == 2 && Time.time - collisionTime < 2.5 )
            {
                OnPlayerPassed();
            }
        }
        else
        {
            if( col == 1 && lastCollision == 2 && Time.time - collisionTime < 2.5 )
            {
                OnPlayerPassed();
            }
        }
        
    }
    void TurnRandom()
    {
        transform.rotation = Quaternion.Euler( 0, 0, Random.Range(0,360) );
    }

    void OnPlayerPassed()
    {
        gameManager.GetScore();
        StartCoroutine( DestroyPoint() );   
    }

    IEnumerator DestroyPoint()
    {
        //Particle falan çağır yok et
        GetComponent<Animator>().SetBool("destroy", true);
        
        yield return new WaitForSeconds(1f);

        FindObjectOfType<CheckPointManager>().RemovePointFromlist(this.gameObject);
        Destroy(gameObject);
    }
}
