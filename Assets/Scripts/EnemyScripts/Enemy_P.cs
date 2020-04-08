using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_P : MonoBehaviour
{
    [SerializeField ]List<GameObject> nextEnemiesPrefabs;
    void OnDestroy()
    {
        SurvivalEnemyManager manager = FindObjectOfType<SurvivalEnemyManager>();
        foreach (GameObject item in nextEnemiesPrefabs)
        {
            FindObjectOfType<SurvivalEnemyManager>().CreateEnemy(4,transform.position, manager.onEnemyCreated);
        }
    }
}
