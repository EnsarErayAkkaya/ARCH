using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public List<GameObject> enemys;
    private GameObject spawnEnemyParticle;
    public int minEnemyCount = 2,maxEnemyCount = 4;
    public int ChooseEnemyCount()
    {
        int a = FindObjectOfType<Player>().howManyRoomVisited;
        if(a <= 2)
        {
            return UnityEngine.Random.Range(minEnemyCount,maxEnemyCount+1);
        }
        else  if(a > 2 && a<=5)
        {
            return UnityEngine.Random.Range(minEnemyCount*2,(maxEnemyCount+1)*2);
        }
        else  if(a > 5 )
        {
            return UnityEngine.Random.Range(minEnemyCount*3,(maxEnemyCount+1)*3);
        }
        else
        {
            return 5;
        }
    }

    public void CallSpawnEnemy(Vector2 pos,RoomController room)
    {
        StartCoroutine(CreateEnemy(pos,room, onEnemyCreated));
    }
   
    public IEnumerator CreateEnemy(Vector2 pos,RoomController room, Action<GameObject,RoomController> onEnemyCreated)
    {
        int i = UnityEngine.Random.Range(0,enemys.Count);

        var particle = Instantiate(enemys[i].GetComponent<Enemy>().spawnParticle,pos,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
    
        yield return new WaitForSeconds(2.5f);
            
        GameObject enemy = SpawnEnemy(i,pos);
        onEnemyCreated(enemy,room);
    }
     public GameObject SpawnEnemy(Vector2 pos)
    {
        return Instantiate(enemys[UnityEngine.Random.Range(0,enemys.Count)],pos,Quaternion.identity);
    }
    public GameObject SpawnEnemy(int index,Vector2 pos)
    {
        return Instantiate(enemys[index],pos,Quaternion.identity);
    }
    void onEnemyCreated(GameObject enemy,RoomController room)
    {
        Debug.Log("onEnemyCreated");
        enemy.GetComponent<Enemy>().room = room;
        room.StoryEnemysInList(enemy);
    }
}
