using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPowerUpController : MonoBehaviour
{
    public bool lifeSteal = false,freezingShot = false;
    void Start()
    {
        SetPassivePowerUps();
    }
    
    public void SetPassivePowerUps()
    {
        foreach (var item in PowerUpManager.powerUpManager.playerPowerUps)
        {
            switch (item)
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
