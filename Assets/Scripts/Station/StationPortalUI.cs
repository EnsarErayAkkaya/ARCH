using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPortalUI : MonoBehaviour
{
    public GameObject floorListParent,floorUIObject,portalOptions,floorListPanel;
    FloorManager floorManager;
    public StationPortal stationPortal;
    void Start()
    {
        floorManager = FindObjectOfType<FloorManager>();
    }
    public void SetFloorsUI()
    {
        CleanList(floorListParent.transform);

        List<Floor> fl = new List<Floor>();
        if(stationPortal.station.room.isEndingRoom)
        {
            fl = floorManager.GetFloorList(true);
        }
        else if(stationPortal.station.room.isStartingRoom)
        {
            fl = floorManager.GetFloorList(false);
        }
        foreach (var item in fl)
        {
            var obj = Instantiate(floorUIObject).GetComponent<FloorUIObject>();
            obj.set(item);
            obj.transform.SetParent(floorListParent.transform);
        }
    }

    public void PortalOptionsActive(bool isActive)
    {
        portalOptions.SetActive(isActive);
    }
    public void FloorsListActive(bool isActive)
    {
        floorListPanel.SetActive(isActive);
    }
    public void CleanList(Transform listItem)
    {
        foreach (Transform item in listItem )
        {
            Destroy(item.gameObject);
        }
    }
    public void CreateNewDungeon()
    {
        FindObjectOfType<FloorListUIController>().Set();
        PortalOptionsActive(false);
        FloorsListActive(false);
        floorManager.CreateNewFloor();
    }
}
