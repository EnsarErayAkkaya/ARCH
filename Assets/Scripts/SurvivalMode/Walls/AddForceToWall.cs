using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToWall : MonoBehaviour
{
    public bool workOnStart = true;
    public float forceMultiplier;
    private float x,y;
    Vector2 forceVector;
    bool AddForce = false;
    Rigidbody2D rb;
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        if(workOnStart)
            StartCoroutine( waitaLitle() );
    }
    IEnumerator waitaLitle()
    {
        yield return new WaitForSeconds(.3f);
        StartForce();
    }
    public void StartForce()
    {
        AddForce = true;
        x = Random.Range(-9,10);
        y = Random.Range(-9,10);
        forceVector = new Vector2( x, y );
    }
    void OnEnable()
    {
       StartForce();
    }

    void FixedUpdate()
    {
        if(AddForce)
        {
            rb.AddForce( forceVector * forceMultiplier, ForceMode2D.Impulse );
            AddForce = false;
        }
    }
}
