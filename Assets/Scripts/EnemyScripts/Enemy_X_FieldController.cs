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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !justStunned )
        {
            StartCoroutine(StunPlayer(other.gameObject.GetComponent< Player_Shoot>() ));
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") )
        {
            justStunned = false;
        }
    }

    IEnumerator StunPlayer(Player_Shoot player)
    {
        player.canShoot = false;
        path.canMove = false;
        justStunned = true;
        enemyX.CallTransform();

        player.gameObject.GetComponent<Player>().GetDamage(damage);

        yield return new WaitForSeconds(2);

        player.canShoot = true;

        yield return new WaitForSeconds(3);

        path.canMove = true;
    }

}
