using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesUIManager : MonoBehaviour
{
    [SerializeField] GameObject PowerUpCardPrefab,content;

    void OnEnable()
    {
        CreateAllPowerUpCards();
    }

    public void CreateAllPowerUpCards()
    {
        //Clean first
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (PowerUp item in PowerUpManager.powerUpManager.powerUps)
        {
            UpgradesUI up = Instantiate(PowerUpCardPrefab).GetComponent<UpgradesUI>();

            bool type = PowerUpManager.powerUpManager.playerPowerUps.Any( s => s == item.powerUpType);
            if(type == true)
            {
                //true döndüğüne göre satılmış demektir.
                up.buyButton.enabled = false;
                up.buyButtonText.text = up.soldString;
            }
            else
            {
                //false döndüyse satılmamış demektir.
                up.buyButtonText.text = up.notSoldString;
            }
            up.powerUpType = item.powerUpType;
            up.powerUpName.text = item.powerUpName;
            up.HiglightButton.image.sprite = item.sprite;
            up.transform.SetParent(content.transform);
            up.priceText.text = item.price.ToString();
            up.description.text = item.description;
        }
    }
}
