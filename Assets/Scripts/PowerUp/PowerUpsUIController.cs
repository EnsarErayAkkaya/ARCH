using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpsUIController : MonoBehaviour
{
    public Transform playerHasContent,playerSelectedContent;
    public GameObject powerUpObject;
    PowerUpManager powerUpManager;

    void OnEnable()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        SetUpObtainedPowerUpsUI();
        SetUpSelectedPowerUpsUI();
    }
    public void SetUpObtainedPowerUpsUI()
    {
        CleanList(playerHasContent);
        foreach (var item in powerUpManager.playerPowerUps)
        {   
            var powerUp = Instantiate(powerUpObject,transform.position,Quaternion.identity).GetComponent<PowerUpUIObject>();
            powerUp.powerUp = PowerUpManager.powerUpManager.powerUps.FirstOrDefault( s => s.powerUpType == item);
            powerUp.SetObjectUI();
            if(powerUp.powerUp.isSelected == false)
            {
                powerUp.transform.GetChild(0).GetComponent<Image>().color = new Color(0.784f,0.317f,0.317f);
            }
            else
            {
                powerUp.transform.GetChild(0).GetComponent<Image>().color = new Color(0.305f,0.635f,0.176f);
            }
            powerUp.transform.SetParent(playerHasContent);
        }
    }
    //Updates the list of obtained powerUps
    public void UpdateObtainedPowerUpsUI(PowerUp powerUp)
    {
        var pUO = Instantiate(powerUpObject,transform.position,Quaternion.identity).GetComponent<PowerUpUIObject>();
        pUO.powerUp = powerUp;
        pUO.SetObjectUI();
        pUO.transform.SetParent(playerHasContent);
    }
    public void SetUpSelectedPowerUpsUI()
    {
        CleanList(playerSelectedContent);
        foreach (var item in powerUpManager.selectedActivePowerUps)
        {
            var powerUp = Instantiate(powerUpObject,transform.position,Quaternion.identity).GetComponent<PowerUpUIObject>();
            powerUp.powerUp = item;
            powerUp.SetObjectUI();
            powerUp.transform.GetChild(0).GetComponent<Image>().color = new Color(0.305f,0.635f,0.176f);
            powerUp.transform.GetChild(0).GetComponent<Button>().enabled = false;
            powerUp.transform.SetParent(playerSelectedContent);
        }
    }
    public void CleanList(Transform listItem)
    {
        foreach (Transform item in listItem )
        {
            Destroy(item.gameObject);
        }
    }
    
}
