using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPortal : MonoBehaviour
{
    FloorManager floorManager;
    public StationObject station;
    StationPortalUI StationPortalUI;
    void Start()
    {
        floorManager = FindObjectOfType<FloorManager>();
        StationPortalUI = FindObjectOfType<StationPortalUI>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if( station.room.roomCleaned )
            {
                StationPortalUI.stationPortal = this;
                StationPortalUI.PortalOptionsActive(true);
                //stationManager.ConnectStation(transform);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        StationPortalUI.stationPortal = null;
        StationPortalUI.FloorsListActive(false);
        StationPortalUI.PortalOptionsActive(false);
    }
}
