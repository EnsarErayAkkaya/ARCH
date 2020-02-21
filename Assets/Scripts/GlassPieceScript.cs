using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPieceScript : MonoBehaviour
{
    public Sprite[] pieces;
    public int damage;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage( damage );
        }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = pieces[Random.Range(0,pieces.Length)];
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
