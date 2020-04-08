using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_P : MonoBehaviour
{   
    int waveIndex;
    [SerializeField ]List<GameObject> nextEnemiesPrefabs;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        waveIndex = FindObjectOfType<SurvivalGameManager>().waveIndex;
    }
    void OnDestroy()
    {
        if(waveIndex != FindObjectOfType<SurvivalGameManager>().waveIndex 
            || FindObjectOfType<SurvivalGameManager>() == null 
                || FindObjectOfType<SurvivalGameManager>().isGameStarted == false)
            return;
        foreach (GameObject item in nextEnemiesPrefabs)
        {
            Transform t = Instantiate(item,transform.position,Quaternion.identity).transform;
            t.SetParent(FindObjectOfType<SurvivalEnemyManager>().enemysParent);
        }
    }
}
