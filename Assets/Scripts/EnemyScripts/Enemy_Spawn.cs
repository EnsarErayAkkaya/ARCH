using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject enemy_prefab;
    public float spawnRate;
    public int life,EnemyCountToProduce;
    public bool produceLimitedEnemy;
    private int producedEnemyCount = 0;
    void Start()
    {
        InvokeRepeating("Spawn",spawnRate,spawnRate);
    }
    public void Spawn()
    {
        if(produceLimitedEnemy)
        {
            if( producedEnemyCount < EnemyCountToProduce)
            {
                producedEnemyCount++;
                Instantiate(enemy_prefab,transform.position,Quaternion.identity);
            }
        }
        else
        {
            Instantiate(enemy_prefab,transform.position,Quaternion.identity);
        } 
    }
    public void GetDamage(int damage)
    {
        life -= damage;
        DestroyNest();
    }
    public void DestroyNest()
    {
        if(life<1)
        {
            Destroy(gameObject);
        }
    }
}
