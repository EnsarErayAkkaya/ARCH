using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    SurvivalGameManager gameManager;
    int lastCollision;
    float collisionTime;
    public GameObject arrow;
    public Collider2D rightCollider;
    public ParticleSystem leftParticle,rightParticle;
    public SpriteRenderer up,down;
    //sadece önce sağdan geçildiğinde kabul et demek
    public bool isOneSided;
    [SerializeField] float ringAnimDist;
    [SerializeField] GameObject ringAnimPrefab;
    void Start()
    {
        //Random belirle tek taraflı mı değil mi
        if(Random.Range(0,2)>0) isOneSided = true;

        TurnRandom();

        gameManager = FindObjectOfType<SurvivalGameManager>();
        
        if(isOneSided)
        {
            leftParticle.gameObject.SetActive(false);
            arrow.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!isOneSided)
            {
                if(rightCollider )
                {
                    if(lastCollision == 2 && Time.time - collisionTime < 2.5 )
                    {
                        OnPlayerPassed();
                    }
                    //1 sol
                    lastCollision = 1;
                    collisionTime = Time.time;
                }
                if( rightCollider )
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
                if( rightCollider )
                {
                    //1 sol
                    lastCollision = 1;
                    collisionTime = Time.time;
                }
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

        FindObjectOfType<CheckPointManager>().RemovePointFromlist(this);
        Destroy(gameObject);
    }
    public void AddRing(int i)
    {
        Vector3 pos;
        GameObject obj;
        // çiftse sola
        if(i % 2 == 0)
        {
            int bolum = i / 2;
            pos = new Vector3(-ringAnimDist *(bolum), 0, 0);
            obj = Instantiate(ringAnimPrefab);
        }
        // tekse sağa
        else
        {
            int bolum = i / 2;
            int kalan = i % 2;
            pos = new Vector3(ringAnimDist *(bolum+ kalan), 0, 0);
            obj = Instantiate(ringAnimPrefab);
        }
        obj.transform.SetParent(this.transform);

        obj.transform.localPosition = pos;

        obj.transform.rotation = this.transform.rotation;

        obj.transform.localScale = new Vector3(1,1,1);

        obj.GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor"
            , up.material.GetColor("_EmissionColor"));
    }
}
