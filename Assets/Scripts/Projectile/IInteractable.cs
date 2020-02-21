using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void OnTriggerEnter2D(Collider2D other);
    void Interract();
    void DestroyInteractable();
}
