using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradesUI : MonoBehaviour
{
    [SerializeField]PowerUpType powerUpType;
    [SerializeField]int price;
    [SerializeField]TextMeshProUGUI description,priceText;
    bool isHiglighted = false;
    void Start()
    {
        priceText.text = price.ToString();
    }
    public void onBuyButtonClick()
    {
        int coin = SaveAndLoadGameData.instance.savedData.coin;
        if(coin >= price)
        {
            coin -= price;
            FindObjectOfType<PowerUpManager>().ObtainPower(powerUpType);
            SaveAndLoadGameData.instance.savedData.coin = coin;
            SaveAndLoadGameData.instance.Save();
        }
        else
        {
            Debug.Log("Unsufficiant coin");
        }
    }
    public void onUpgradeClick()
    {
        //Kartı öne getir ve arka planı buğulaştır.
        //Açıklamasını göster.
        if(!isHiglighted)
        {
            description.gameObject.SetActive(true);
            isHiglighted = true;
        }
        else
        {
            description.gameObject.SetActive(false);
            isHiglighted = false;
        }
    }

}
