using System;
using UnityEngine;

[Serializable]
public class ProjectilesPacket
{
    public PacketType packet;
    public GameObject normalProjectile,middleProjectile,powerfulProjectile;
}
public enum PacketType
{
    Default
}
