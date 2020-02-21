using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Room 
{
    public List<Dir2> directions;
    public Dir2 pos;
    public bool isStartingRoom,isEndingRoom,roomCleaned;
    public Room(Vector2 p)
    {
        directions = new List<Dir2>();
        pos = (Dir2)p;
    }
}
