using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void OnCollisionEnter2D(Collision2D other);    
    void Interract();
    void DestroyInteractable();
}
