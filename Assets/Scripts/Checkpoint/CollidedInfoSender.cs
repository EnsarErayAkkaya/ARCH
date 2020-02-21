using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidedInfoSender : MonoBehaviour
{
    public CheckpointController checkpointController;
    public bool right; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            int col;
            if(right)
                col = 2;
            else
                col = 1;
            checkpointController.Collided(col,Time.time);
        }
    }
}
