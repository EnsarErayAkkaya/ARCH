using UnityEngine;
using UnityEngine.UI;

public class ActivePowerUpGameUI : MonoBehaviour
{
    [SerializeField]Image image;

    PowerUp power;
    void Start()
    {
        power = PowerUpManager.powerUpManager.selectedActivePowerUps[0];
        Set();
    }   
    void Set()
    {
        image.sprite = power.sprite;
    }
    public void Use()
    {
        PowerUpManager.powerUpManager.GivePower(power);
    }
}
