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
    public bool isSelected;
    public int price;
}
public enum PowerUpType
{
    MachineGun,LifeStealing,FreezingShot
}
public enum UsageType
{
    Temporary,Permanent
}