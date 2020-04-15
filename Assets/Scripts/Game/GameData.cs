using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData 
{
    public int score,totalScore,coin,playedGameCount;
    public List<PowerUpType> playerPowerUps = new List<PowerUpType>();
    public List<PowerUpType> selectedActivePowerUps = new List<PowerUpType>();
    public bool isGlow;

    /* public int currentFloor;
    public int maxReachedFloor;
    public List<Floor> floors;
    public List<Passanger> Passangers;
    public PlayerData playerData;
    public GameData()
    {
        currentFloor = 0;
        maxReachedFloor = 0;
        floors = new List<Floor>();
        Passangers = new List<Passanger>();
        playerData = new PlayerData();
    } */
}
/* 
[Serializable]
public class PlayerData 
{
    public float positionX,positionY;
	public int killedEnemyCount;
	public float currentHealth;
	public int maxHealth;
	public int howManyRoomVisited;
    public PlayerData()
    {
        killedEnemyCount = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
        howManyRoomVisited = 0;
    }
     public PlayerData(int _killedEnemyCount, int _maxHealth, float _currentHealth, int _howManyRoomVisited, float _positionX, float _positionY )
    {
        positionX = _positionX;
        positionY = _positionY;
        killedEnemyCount = _killedEnemyCount;
        maxHealth = _maxHealth;
        if(_currentHealth == 0)
        {
            currentHealth = _maxHealth;
        }
        else{
            currentHealth = _currentHealth;
        }
        howManyRoomVisited = _howManyRoomVisited;
    }
}
 [Serializable]
    public class Dir2
    {
        float x,y;
        public Dir2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    public static implicit operator Vector2(Dir2 d)
    {
        Vector2 vector2 = new Vector2(d.x, d.y);
        return vector2;
    }
    public static explicit operator Dir2(Vector2 b)
    {
        return new Dir2(b.x, b.y);
    }
}  */