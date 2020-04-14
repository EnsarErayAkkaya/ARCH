using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyFieldController : MonoBehaviour
{
    bool playerInside;
    [SerializeField]
    float damageOnSecond;
    [SerializeField] 
    Vector3 startScale;
    SurvivalGameManager gameManager;
    [SerializeField]Player p;
    void Start()
    {
        gameManager = FindObjectOfType<SurvivalGameManager>();
    }

    public void ResetField()
    {
        
        gameObject.transform.localScale = startScale;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            StartCoroutine( DamagePlayerByTime(p) );
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    IEnumerator DamagePlayerByTime(Player p)
    {
        float t = 0.0f,lastDamageTime = 0.0f;
        while(playerInside)
        {
            t += Time.deltaTime;
            if( lastDamageTime+Time.deltaTime < t && gameManager.isGameStarted && !gameManager.gameStopped )
            {
                p.GetDamage( damageOnSecond );
                lastDamageTime = t;
            }
            yield return null;
        }
    }
}
