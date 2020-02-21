using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public int minRoomCount,maxRoomCount;
    
    public int currentFloor = 0,maxReachedFloor = 0;
    public List<Floor> floors = new List<Floor>();
    RoomManager roomManager;
    StationManager stationManager;
    //Transform player;
    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
       // player = FindObjectOfType<Player>().transform;
        stationManager = FindObjectOfType<StationManager>();

        GetLastSettings();
        
        if(currentFloor == 0 && maxReachedFloor == 0)
        {
            CreateNewFloor();
        }
        else
        {
            CreateOldFloor(currentFloor);
        }
        
    }
    
    public void GoToNextFloor()
    {
        /*Görsel efektler çalışır
        Oyuncu Vector2.zero konumuna transport olur recoili de oraya aktarılır
        Şu an oluşturlmuş odalar yok edilir ve yenileri oluşturulur */
        if(currentFloor <= maxReachedFloor)
        {
            CreateOldFloor(currentFloor);
        }
        else
        {
            maxReachedFloor++;
            CreateNewFloor();
        }
    }
    
    public void GoToPreviousFloor()
    {
        currentFloor--;
        /*Görsel efektler çalışır
         Oyuncu Vector2.zero konumuna transport olur recoili de oraya aktarılır
         Şu an oluşturlmuş odalar yok edilir ve yenileri oluşturulur */
        CreateOldFloor(currentFloor);
    }
    public void CreateNewFloor()
    {
       
        /* Yeniden odaları yok etmeyi ve oluşturma func. ları çağırıyor. */
        roomManager.choosenRoomCount = Random.Range(minRoomCount,maxRoomCount);
        roomManager.DestroyAllRooms();
        stationManager.DestroyOldStations();
        roomManager.CreateDungeon();
        currentFloor++;
        maxReachedFloor = currentFloor;
        //Bir Kat geçildiğinde oyunu kaydet diyelim.
        FindObjectOfType<GameManager>().CallSaveGame();
    }
    public void CreateOldFloor(int floorIndex)
    {
        bool toUp = false;
        if(floorIndex > currentFloor)
        {
            toUp = true;
        }
        currentFloor = floorIndex;
        stationManager.DestroyOldStations();
        roomManager.DestroyAllRooms();
        roomManager.CreateOldDungeon( floors[floorIndex-1], toUp );
    }
    public void GetLastSettings()
    {
        currentFloor = SaveAndLoadGameData.instance.savedData.currentFloor;
        maxReachedFloor = SaveAndLoadGameData.instance.savedData.maxReachedFloor;
        floors = SaveAndLoadGameData.instance.savedData.floors;
    }

    public List<Floor> GetFloorList(bool toUp)
    {
        List<Floor> fl = new List<Floor>();
        if(toUp)
        {
            for (int i = currentFloor+1; i < floors.Count; i++)
            {
                fl.Add(floors[i]);
            }
        }
        else
        {
            for (int i = currentFloor-2; i >= 0; i--)
            {
                fl.Add(floors[i]);
            }
        }
        return fl;
    }
}
