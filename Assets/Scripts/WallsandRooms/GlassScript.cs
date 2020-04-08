using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    public int glassHealth = 3;
    public GameObject brokenGlass,brokenGlass2,glassPiece;
    public bool hasBomb, canEffectFromBomb, dontTakeDamageFromProjectiles;
  
    void Start()
    {
        //If canEffectFromBomb == true then it cant move 
        if(canEffectFromBomb)
        {
            FindObjectOfType<CreateRandomWalls>().CreateAWall(1);
            GetComponent<AddForceToWall>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        brokenGlass.SetActive(false);
        brokenGlass2.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Projectile") && dontTakeDamageFromProjectiles == false )
		{
            if(other.gameObject.GetComponent<Projectile>() != null)
            {
                other.gameObject.GetComponent<Projectile>().life = 0;
                other.gameObject.GetComponent<Projectile>().CollisionInterract(gameObject);
                glassHealth--;
                CrackTheGlass();
            }
            if(other.gameObject.GetComponent<Enemy_Projectile>() != null)
            {
                other.gameObject.GetComponent<Enemy_Projectile>().DestroyProjectile();
            }
        }
    }
    public void GetDamageFromBomb()
    {
        if(canEffectFromBomb)
        {
            glassHealth = 0;
            CrackTheGlass();
        }
    }
    void CrackTheGlass()
    {
        if( glassHealth == 2 )
        {
            FindObjectOfType<AudioManager>().Play( "GlassCrack" );
            brokenGlass.SetActive(true);
        }
        if( glassHealth == 1 )
        {
            FindObjectOfType<AudioManager>().Play( "GlassCrack" );
            brokenGlass2.SetActive(true);
        }
        else if( glassHealth == 0 )
        {
            FindObjectOfType<AudioManager>().Play( "GlassBreak" );

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
            glass.transform.SetParent(FindObjectOfType<CreateRandomWalls>().wallsParent.transform);
            if( !hasBomb )
                glass.GetComponent<AddForceToWall>().AddForce = true;
        }
    }
}
