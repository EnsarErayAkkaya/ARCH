using UnityEngine;
using TMPro;

public class FloorUIObject : MonoBehaviour
{
    private Floor floor;
    public TextMeshProUGUI floorNameUI;
    StationPortalUI stationPortalUI;
    public void set( Floor fl)
    {
        stationPortalUI = FindObjectOfType<StationPortalUI>();
        floor = fl;
        floorNameUI.text = fl.floorName;
    }
    public void OnClick()
    {
        FindObjectOfType<FloorManager>().CreateOldFloor(floor.floorIndex);
        stationPortalUI.FloorsListActive(false);
        stationPortalUI.PortalOptionsActive(false);
    }
}
