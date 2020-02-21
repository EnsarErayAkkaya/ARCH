using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float torqueMultiplier;
    
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
        TurnWithForce();
    }
    public void TurnWithForce()
    {
        float turnDir = -Input.GetAxis("Horizontal");
        rb.AddTorque( turnDir * torqueMultiplier );
    }

}
