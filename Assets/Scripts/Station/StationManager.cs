using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    Player_Shoot player;
    StationUI stationUI;
    FloorManager floorManager;
    public StationObject connectedStation;
    public GameObject station;
    void Awake()
    {
        player = FindObjectOfType<Player_Shoot>();
        stationUI = FindObjectOfType<StationUI>();
        floorManager = FindObjectOfType<FloorManager>();
    }
    public void ConnectStation()
    {
        player.canShoot = false;
        player.canRecoil = false;
        player.transform.position =  connectedStation.transform.position + new Vector3(8,2.5f);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //stationUI.ActivateStationOptionsPanel();
        CallUpdatePassangerCountText();
    }
    public void ReleasePlayer()
    {
        //connectedStation = null;
        player.canShoot = true;
        player.canRecoil = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //Oyunu katdet
        FindObjectOfType<GameManager>().CallSaveGame();
    }
    public void DestroyOldStations()
    {
        foreach (var item in FindObjectsOfType<StationObject>())
        {
            if(item.floor.floorIndex != floorManager.currentFloor)
            {
                Destroy(item.gameObject);
            }
        }
    }
    public void SetConnectedStation(Transform obj)
    {
        connectedStation = obj.GetComponent<StationObject>();
    }
    public void CallUpdatePassangerCountText()
    {
        stationUI.UpdateStationPassangerCountText(connectedStation.passangersInStation);
    }
    public void CreateNewStation(RoomController room, Floor floor)
    {
        //İstasyon Üret
        Vector2 pos = new Vector2(room.transform.position.x -15f, room.transform.position.y +10f );
        var createdStation = Instantiate(station,pos,Quaternion.identity).GetComponent<StationObject>();
        createdStation.room = room;
        createdStation.floor = floor;
        //createdStation.stationFloorIndex =
        createdStation.SetStation(-1);
    }
    public void CreateOldStation(RoomController room, Station stationData)
    {
        //İstasyon Üret
        Vector2 pos = new Vector2(room.transform.position.x -15f, room.transform.position.y +10f );
        var createdStation = Instantiate(station,pos,Quaternion.identity).GetComponent<StationObject>();
        createdStation.room = room;
        createdStation.SetStation(stationData.passangerCount);
    }
}
