using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class PowerUpObject : MonoBehaviour, IInteractable
{
    public PowerUpType powerUpType;
    private PowerUp powerUp;
    private PowerUpManager powerUpManager;
    private int health = 3;
    public bool isRandom;
    public RoomController room;
    public void SetUp()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        if(isRandom)
        {
            Array values = Enum.GetValues(typeof(PowerUpType));
            powerUpType = (PowerUpType)values.GetValue(UnityEngine.Random.Range(0,values.Length));
        }
        powerUp = powerUpManager.powerUps.FirstOrDefault( p => p.powerUpType == powerUpType);
        GetComponent<SpriteRenderer>().sprite = powerUp.sprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            health--;
            other.gameObject.GetComponent<Projectile>().life = 0;
            other.gameObject.GetComponent<Projectile>().Bomb();
            other.gameObject.GetComponent<Projectile>().DestroyProjectile();
            if( health < 1)
            {
                if(room.isEndingRoom == true)
                {
                    FindObjectOfType<Player>().GainHealth(100);
                }
                Interract();
            }
        }
    }
    public void Interract()
    {
        powerUpManager.ObtainPower(powerUp);
        room.PowerUpChoosen(transform.position);
        DestroyInteractable();
    }
    
    public void DestroyInteractable()
    {
        var part = Instantiate(powerUp.particle,transform.position,Quaternion.identity);
        part.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
