using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIObject : MonoBehaviour
{
    public PowerUp powerUp;
    [SerializeField]
    private Image powerUpSprite;
    [SerializeField]
    private TextMeshProUGUI powerUpName;
    
    public void SetObjectUI()
    {
        powerUpSprite.sprite = powerUp.sprite;
        powerUpName.text = powerUp.powerUpName;
    }
    public void onClick()
    {
        PowerUpManager powerUpManager = FindObjectOfType<PowerUpManager>();

        if(powerUp.isSelected == false)
        {
            powerUpManager.SelectPowerUp(powerUp);
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.305f,0.635f,0.176f);
            powerUp.isSelected = true;
        }
        else
        {
            powerUpManager.DeselectPowerUp(powerUp);
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.784f,0.317f,0.317f);
            powerUp.isSelected = false;
        }
        FindObjectOfType<PowerUpsUIController>().SetUpSelectedPowerUpsUI();
    }
}
