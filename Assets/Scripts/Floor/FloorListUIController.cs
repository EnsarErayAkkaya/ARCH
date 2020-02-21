using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorListUIController : MonoBehaviour
{
    StationPortalUI stationPortalUI;
    public GameObject nextGalaxyButton;
    void OnEnable()
    {
        Set();
    }
    public void Set()
    {
        stationPortalUI = FindObjectOfType<StationPortalUI>();
        stationPortalUI.SetFloorsUI();
        if(stationPortalUI.stationPortal.station.room.isStartingRoom == true)
        {
            nextGalaxyButton.SetActive(false);
        }
        if(stationPortalUI.stationPortal.station.room.isEndingRoom == true)
        {
            nextGalaxyButton.SetActive(true);
        }
    }
}
