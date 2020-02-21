using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPowerUpController : MonoBehaviour
{
    PowerUpManager powerUpManager;
    public bool lifeSteal = false,freezingShot = false;
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }
    
    public void SetPassivePowerUps()
    {
        lifeSteal = false;
        foreach (var item in powerUpManager.selectedPassivePowerUps)
        {
            switch (item.powerUpType)
            {
                case PowerUpType.LifeStealing:
                    lifeSteal = true;
                break;

                case PowerUpType.FreezingShot:
                    freezingShot = true;
                break;

                default:
                break;
            }
        }
    }
}
