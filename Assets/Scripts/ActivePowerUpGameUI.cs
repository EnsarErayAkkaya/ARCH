using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePowerUpGameUI : MonoBehaviour
{
    [SerializeField] Button useButton;
    [SerializeField]Image image;
    [SerializeField] TextMeshProUGUI cooldownText;
    PowerUp power;
    float cooldowntime;
    void Start()
    {
        if(PowerUpManager.powerUpManager.selectedActivePowerUps.Count <1)
        {
            useButton.gameObject.SetActive(false);
        }
        else
        {
             power = PowerUpManager.powerUpManager.powerUps
            .FirstOrDefault(
                s => 
                s.powerUpType == PowerUpManager.powerUpManager.selectedActivePowerUps[0] );

            Set();
        }
    }

    void Set()
    {
        image.sprite = power.sprite;
        cooldowntime = power.cooldownTime;
    }
    public void Use()
    {
        PowerUpManager.powerUpManager.GivePower(power);
        Debug.Log("bura");
        StartCoroutine( CooldownTimer() );
    }
    IEnumerator CooldownTimer()
    {
        useButton.interactable = false;
        cooldownText.gameObject.SetActive(true);
        Debug.Log("bura0");

        while(cooldowntime > 0)
        {
            cooldowntime -= Time.deltaTime;
            cooldownText.text = cooldowntime.ToString("#.#");
            yield return null;
        }
        Debug.Log("bura1");

        cooldowntime = power.cooldownTime;
        useButton.interactable = true;
        cooldownText.gameObject.SetActive(false);
    }
}
