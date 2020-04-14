using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalEnemyManager : MonoBehaviour
{
    private float radius;
    public List<GameObject> enemys,selectedEnemies,liveEnemies;
    private GameObject spawnEnemyParticle;
    public int enemyCount = 4;
    SurvivalGameManager survivalManager;
    [SerializeField] int O_levelStart,Y_levelStart,X_levelStart,P_levelStart;
    public Transform enemysParent;
    void Start()
    {
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }
    public void ProduceEnemys()
    {
        radius = survivalManager.gameRadius;
        ChooseEnemyCount();
        ChooseEnemysToCreate();
        for (int i = 0; i < enemyCount; i++)
        {
            CallSpawnEnemy( ChooseRandomLocation() );
        }
    }
    public void ChooseEnemyCount()
    {
        if( survivalManager.waveIndex < 5 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(3,5);
        }
        else if( survivalManager.waveIndex < 5 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex >= 5 && survivalManager.waveIndex <10 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(3,7);
        }
        else if( survivalManager.waveIndex >= 5 && survivalManager.waveIndex <10 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(6,10);
        }
        else if( survivalManager.waveIndex >= 10 && radius <= 60 )
        {
            enemyCount = UnityEngine.Random.Range(9,13);
        }
        else if( survivalManager.waveIndex >= 10 && radius > 60 )
        {
            enemyCount = UnityEngine.Random.Range(12,16);
        }
    }
    
    void ChooseEnemysToCreate()
    {
        selectedEnemies.Clear();
        // H enemy added
        selectedEnemies.Add(enemys[0]);
        // O Enemy adding
        if(survivalManager.waveIndex >= O_levelStart )
        {
            selectedEnemies.Add(enemys[1]);
        }
        // Y Enemy adding
        if(survivalManager.waveIndex >= Y_levelStart)
        {
            selectedEnemies.Add(enemys[2]);
        }
        // X enemy adding
        if(survivalManager.waveIndex >= X_levelStart)
        {
            selectedEnemies.Add(enemys[3]);
        }
        if(survivalManager.waveIndex >= P_levelStart)
        {
            selectedEnemies.Add(enemys[4]);
        }
        
    }
    Vector2 ChooseRandomLocation()
    {
        var vector2 = UnityEngine.Random.insideUnitCircle * radius;
        return new Vector2(transform.position.x + vector2.x,transform.position.y + vector2.y);
    }

    public void CallSpawnEnemy(Vector2 pos)
    {
        StartCoroutine(CreateEnemy(pos, onEnemyCreated ));
    }
   
    public IEnumerator CreateEnemy(Vector2 pos, Action<GameObject> onEnemyCreated)
    {
        int i = UnityEngine.Random.Range(0, selectedEnemies.Count);

        var particle = Instantiate( selectedEnemies[i].GetComponent<Enemy>().spawnParticle, pos, Quaternion.identity );
        particle.GetComponent<ParticleSystem>().Play();
    
        yield return new WaitForSeconds(2.5f);
            
        GameObject enemy = SpawnEnemy(i,pos);
        onEnemyCreated(enemy);
    }
    ///<summary>
    ///H = 0, O = 1, Y = 2, X = 3,P = 4.
    ///</summary>
    public GameObject SpawnEnemy(int index,Vector2 pos)
    {
        GameObject e = Instantiate(selectedEnemies[index],pos,Quaternion.identity);
        e.transform.SetParent(enemysParent);
        return e;
    }
    public void onEnemyCreated(GameObject enemy)
    {
        liveEnemies.Add(enemy);
    }
    public void OnEnemyKilled(GameObject enemy)
    {
        liveEnemies.Remove(enemy);
        survivalManager.GetEnemyScore();
        if(liveEnemies.Count == 0)
        {
            survivalManager.GetEnemyScore();
        }
    }
}
