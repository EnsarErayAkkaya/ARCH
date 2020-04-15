using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy_X_FieldController : MonoBehaviour
{
    [SerializeField]
    int damage;
    public AIPath path;
    public Enemy_XController enemyX;
    bool justStunned = false;
    Player_Shoot player;
    Player p;
    void Start()
    {
        player = FindObjectOfType<Player_Shoot>();
        p = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !justStunned )
        {
            StartCoroutine(StunPlayer());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") )
        {
            justStunned = false;
        }
    }

    IEnumerator StunPlayer()
    {
        player.canShoot = false;
        path.canSearch = false;
        path.canMove = false;
        justStunned = true;
        enemyX.CallTransform();

        p.GetDamage(damage);

        yield return new WaitForSeconds(2);

        player.canShoot = true;

        yield return new WaitForSeconds(3);

        path.canMove = true;
        path.canSearch = true;  
    }

}
