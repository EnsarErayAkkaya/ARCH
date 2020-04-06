using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePowerUpGameUI : MonoBehaviour
{
    public Button useButton;
    [SerializeField]Image image;
    [SerializeField] TextMeshProUGUI cooldownText;
    PowerUp power;
    float cooldowntime;
    SurvivalGameManager gameManager;
    void Start()
    {
        if(PowerUpManager.powerUpManager.selectedActivePowerUps.Count <1)
        {
            useButton.gameObject.SetActive(false);
        }
        else
        {
            gameManager = FindObjectOfType<SurvivalGameManager>();
            power = PowerUpManager.powerUpManager.powerUps
            .FirstOrDefault(
                s => 
                s.powerUpType == PowerUpManager.powerUpManager.selectedActivePowerUps[0] );

            Set();
        }
    }
    public void ChangeButtonInteractable(bool a)
    {
        useButton.interactable = a;
    }
    void Set()
    {
        image.sprite = power.sprite;
        cooldowntime = power.cooldownTime;
    }
    public void Use()
    {
        PowerUpManager.powerUpManager.GivePower(power);
        StartCoroutine( CooldownTimer() );
    }
    IEnumerator CooldownTimer()
    {
        useButton.interactable = false;
        cooldownText.gameObject.SetActive(true);

        while(cooldowntime > 0)
        {
            if(gameManager.gameStopped == false && gameManager.isGameStarted == true)
            {
                cooldowntime -= Time.deltaTime;
                cooldownText.text = cooldowntime.ToString("#.#");
            }
            yield return null;
        }

        cooldowntime = power.cooldownTime;
        useButton.interactable = true;
        cooldownText.gameObject.SetActive(false);
    }
}
