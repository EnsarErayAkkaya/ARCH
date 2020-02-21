using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalEnemyManager : MonoBehaviour
{
    private float radius;
    public List<GameObject> enemys;
    private GameObject spawnEnemyParticle;
    public int enemyCount = 4;
    public List<GameObject> liveEnemies = new List<GameObject>();
    SurvivalGameManager survivalManager;
    void Start()
    {
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }
    public void ProduceEnemys()
    {
        radius = FindObjectOfType<SurvivalGameManager>().gameRadius;
        ChooseEnemyCount();
        for (int i = 0; i < enemyCount; i++)
        {
            CallSpawnEnemy( ChooseRandomLocation() );
        }
    }
    public void ChooseEnemyCount()
    {
        if( survivalManager.waveIndex < 5 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(2,5);
        }
        else if( survivalManager.waveIndex < 5 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex > 5 && survivalManager.waveIndex <10 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(6,10);
        }
        else if( survivalManager.waveIndex > 15 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(9,13);
        }
        else if( survivalManager.waveIndex > 15 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(12,16);
        }
    }
     Vector2 ChooseRandomLocation()
    {
        var vector2 = UnityEngine.Random.insideUnitCircle * radius;
        return new Vector2(transform.position.x + vector2.x,transform.position.y + vector2.y);
    }

    public void CallSpawnEnemy(Vector2 pos)
    {
        StartCoroutine(CreateEnemy(pos, onEnemyCreated));
    }
   
    public IEnumerator CreateEnemy(Vector2 pos, Action<GameObject> onEnemyCreated)
    {
        int i = UnityEngine.Random.Range(0,enemys.Count);

        var particle = Instantiate(enemys[i].GetComponent<Enemy>().spawnParticle,pos,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
    
        yield return new WaitForSeconds(2.5f);
            
        GameObject enemy = SpawnEnemy(i,pos);
        onEnemyCreated(enemy);
    }
     public GameObject SpawnEnemy(Vector2 pos)
    {
        return Instantiate(enemys[UnityEngine.Random.Range(0,enemys.Count)],pos,Quaternion.identity);
    }
    public GameObject SpawnEnemy(int index,Vector2 pos)
    {
        return Instantiate(enemys[index],pos,Quaternion.identity);
    }
    void onEnemyCreated(GameObject enemy)
    {
        liveEnemies.Add(enemy);
    }
    public void OnEnemyKilled(GameObject enemy)
    {
        liveEnemies.Remove(enemy);
        FindObjectOfType<SurvivalGameManager>().GetEnemyScore();
        if(liveEnemies.Count == 0)
        {
            FindObjectOfType<SurvivalGameManager>().GetEnemyScore();
        }
    }
}
