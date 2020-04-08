using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_P : MonoBehaviour
{
    [SerializeField ]List<GameObject> nextEnemiesPrefabs;
    void OnDestroy()
    {
        foreach (GameObject item in nextEnemiesPrefabs)
        {
            Instantiate(item,transform.position,Quaternion.identity);
        }
    }
}
