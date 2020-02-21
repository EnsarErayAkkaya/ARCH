using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FloorManager floorManager;
    Player player;
    void Awake()
    {
        player = FindObjectOfType<Player>();
        floorManager = FindObjectOfType<FloorManager>();
    }

    //Saves the Game
    public void CallSaveGame()
    {
        SaveAndLoadGameData.instance.savedData = new GameData() {
            currentFloor = floorManager.currentFloor,
            maxReachedFloor = floorManager.maxReachedFloor,
            floors = floorManager.floors,
            Passangers = FindObjectOfType<PassangerManager>().Passangers,
            playerData = new PlayerData(player.killedEnemyCount
                , player.maxHealth,player.currentHealth,player.howManyRoomVisited, player.transform.position.x, player.transform.position.y)
        };
        SaveAndLoadGameData.instance.Save();
    }
}
