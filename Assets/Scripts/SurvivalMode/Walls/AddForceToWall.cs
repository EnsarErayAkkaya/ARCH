using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToWall : MonoBehaviour
{
    public bool AddForce = false, workOnStart = true;
    public float forceMultiplier;
    private float x,y;
    void Start()
    {
        if(workOnStart)
            StartCoroutine( waitaLitle() );
    }
    IEnumerator waitaLitle()
    {
        yield return new WaitForSeconds(.5f);
        AddForce = true;
    }

    void FixedUpdate()
    {
        if(AddForce)
        {
            x = Random.Range(-9,10);
            y = Random.Range(-9,10);
            Vector2 forceVector = new Vector2( x, y );
            gameObject.GetComponent<Rigidbody2D>().AddForce( forceVector * forceMultiplier, ForceMode2D.Impulse );
            AddForce = false;
        }
    }
}
