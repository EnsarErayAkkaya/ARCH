using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationUI : MonoBehaviour
{
    public GameObject StationPanel,StationOptionsPanel;
    StationManager stationManager;
    public TextMeshProUGUI stationPassangerCountText;
    void Start()
    {
        stationManager = FindObjectOfType<StationManager>();
    }

    public void ActivateStationOptionsPanel()
    {
        StationOptionsPanel.SetActive(true);
    }
    public void CloseStationOptionsPanel()
    {
        stationManager.ReleasePlayer();
        StationOptionsPanel.SetActive(false);
    }

    public void ActivateStationPanel()
    {
        stationManager.ConnectStation();
        StationPanel.SetActive(true);
    }
    public void CloseStationPanel()
    {
        stationManager.ReleasePlayer();
        StationPanel.SetActive(false);
    }
    public void UpdateStationPassangerCountText(int passangerCount)
    {
        stationPassangerCountText.text = passangerCount.ToString();
    }
    
}
