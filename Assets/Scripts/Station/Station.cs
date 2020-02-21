using System;
using UnityEngine;

[Serializable]
public class Station 
{
    public int floorIndex;
    public int passangerCount;
    public Station(int fl, int pasCount)
    {
        floorIndex = fl;
        passangerCount = pasCount;
    }
}
