using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationObject : MonoBehaviour
{
    StationManager stationManager;
    StationUI stationUI;
    public RoomController room;
    public int minPassangerInStation,maxPassangerInStation;
    public int passangersInStation;
    public Floor floor;
    public Station station;
    void Start()
    {
        stationManager = FindObjectOfType<StationManager>();
        stationUI = FindObjectOfType<StationUI>();
    }
    public void SetStation(int i)
    {
        if(i == -1)
        {
            passangersInStation = Random.Range(minPassangerInStation,maxPassangerInStation);
            //Station oluşturuluyor
            station = new Station(floor.floorIndex, passangersInStation);
            floor.stations.Add(station);
        }
        else{
            passangersInStation = i;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(room != null)
            {
                if( room.roomCleaned )
                {
                    stationUI.ActivateStationOptionsPanel();
                    //stationManager.ConnectStation(transform);
                }
            }
            else{
                stationUI.ActivateStationOptionsPanel();
                //stationManager.ConnectStation(transform);
            }
            stationManager.SetConnectedStation(transform);
        }
    }
  
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            stationManager.connectedStation = null;
            stationUI.CloseStationOptionsPanel();
        }
    }
}
