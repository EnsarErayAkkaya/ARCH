using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PowerUp
{
    public string powerUpName, description;
    public Sprite sprite;
    public PowerUpType powerUpType;
    public UsageType usageType;
    public float usingTime,cooldownTime;
    public List<float> tempData = new List<float>();
    public int price,neededScore;
    public GameObject neededPrefab;
}
public enum PowerUpType
{
    MachineGun,LifeStealing,FreezingShot,UnPerfectShield
}
public enum UsageType
{
    Temporary,Permanent
}