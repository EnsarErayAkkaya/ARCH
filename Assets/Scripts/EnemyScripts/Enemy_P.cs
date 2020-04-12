using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_P : MonoBehaviour
{   
    int waveIndex;
    [SerializeField ]List<GameObject> nextEnemiesPrefabs;
    SurvivalGameManager survivalManager;
    SurvivalEnemyManager enemyManager;
    void Start()
    {
        survivalManager = FindObjectOfType<SurvivalGameManager>();
        enemyManager = FindObjectOfType<SurvivalEnemyManager>();
        waveIndex = survivalManager.waveIndex;
    }
    void OnDestroy()
    {
        if(waveIndex !=survivalManager.waveIndex 
            || survivalManager == null 
                || survivalManager.isGameStarted == false)
            return;
        foreach (GameObject item in nextEnemiesPrefabs)
        {
            Transform t = Instantiate(item,transform.position,Quaternion.identity).transform;
            t.SetParent(enemyManager.enemysParent);
        }
    }
}
