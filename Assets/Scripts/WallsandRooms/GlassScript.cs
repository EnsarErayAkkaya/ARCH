using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    public int glassHealth = 3;
    public GameObject brokenGlass,brokenGlass2,glassPiece;
    public bool hasBomb;
  
    void Start()
    {
        brokenGlass.SetActive(false);
        brokenGlass2.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Projectile"))
		{
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                other.gameObject.GetComponent<Projectile>().life = 0;
                other.gameObject.GetComponent<Projectile>().Bomb();
                other.gameObject.GetComponent<Projectile>().DestroyProjectile();
                glassHealth--;
                CrackTheGlass();
            }
            if(other.gameObject.GetComponent<Enemy_Projectile>() != null)
            {
                other.gameObject.GetComponent<Enemy_Projectile>().DestroyProjectile();
            }
        }
    }
    void CrackTheGlass()
    {
        if( glassHealth == 2 )
        {
            brokenGlass.SetActive(true);
        }
        if( glassHealth == 1 )
        {
            brokenGlass2.SetActive(true);
        }
        else if( glassHealth == 0 )
        {
            FindObjectOfType<AudioManager>().Play( "GlassCrack" );

            CreateGlassPieces();

            brokenGlass.SetActive(false);
            brokenGlass2.SetActive(false);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            if(hasBomb)
            {
                foreach (Transform item in transform)
                {
                    if( item.GetComponent<BombController>() )
                        item.GetComponent<BombController>().StartExplosion();
                }
            }
            
        }
    }
    void CreateGlassPieces()
    {
        for (int i = 0; i < Random.Range(3,7); i++)
        {
            Vector2 pos = new Vector2( Random.Range(-2,3), Random.Range(-2,3) );
            GameObject glass = Instantiate(glassPiece, (Vector2)transform.position + pos, Quaternion.identity);
            if( !hasBomb )
                glass.GetComponent<AddForceToWall>().AddForce = true;
        }
    }
}
