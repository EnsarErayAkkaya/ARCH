using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyFieldController : MonoBehaviour
{
    bool playerInside;
    [SerializeField]
    int damageOnSecond;
    [SerializeField]Color color1,color2;
    void Start()
    {
        //StartCoroutine( ChageBetwenColorByTime() );   
    }

    IEnumerator ChageBetwenColorByTime()
    {
        float t = 0.0f,changeStartTime = 0;
        while(true)
        {
            t += Time.deltaTime / 3;
            changeStartTime = t;
            while( changeStartTime+3 > t )
            {
                t += Time.deltaTime / 3;
                GetComponent<SpriteRenderer>().color 
                    = Color.Lerp(GetComponent<SpriteRenderer>().color, color1, t);
            }
            changeStartTime = t;
            while( changeStartTime+3 > t )
            {
                t += Time.deltaTime / 3;
                GetComponent<SpriteRenderer>().color 
                    = Color.Lerp(GetComponent<SpriteRenderer>().color, color2, t);
            }
            yield return 0;
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            Player p = other.gameObject.GetComponent<Player>();
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
            if( lastDamageTime+Time.deltaTime < t )
            {
                p.GetDamage( damageOnSecond );
                lastDamageTime = t;
            }
            yield return null;
        }
    }
}
