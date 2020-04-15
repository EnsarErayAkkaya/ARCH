using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UpgradesUI : MonoBehaviour
{
    public PowerUpType powerUpType;
    public TextMeshProUGUI description,priceText,powerUpName,buyButtonText,scoreNotEnoughText;
    public Button HiglightButton, buyButton;
    public string soldString,notSoldString,scoreNotEnoughString;

    bool isHiglighted = false;
    public void onBuyButtonClick()
    {
        PowerUp p = PowerUpManager.powerUpManager.powerUps.FirstOrDefault(s => s.powerUpType == this.powerUpType);
        if(p.neededScore != 0 && SaveAndLoadGameData.instance.savedData.totalScore < p.neededScore)
        {
            scoreNotEnoughText.gameObject.SetActive(true);
            scoreNotEnoughText.text = scoreNotEnoughString + " " + p.neededScore +".";
            return;
        }
        int coin = SaveAndLoadGameData.instance.savedData.coin;
        if(coin >= p.price)
        {
            coin -= p.price;
            FindObjectOfType<PowerUpManager>().ObtainPower(powerUpType);
            //
            bool type = PowerUpManager.powerUpManager.playerPowerUps.Any( s => s == powerUpType);
            if(type == true)
            {
                //true döndüğüne göre satılmış demektir.
                buyButton.enabled = false;
                buyButtonText.text = soldString;
            }
            else
            {
                //false döndüyse satılmamış demektir.
                buyButtonText.text = notSoldString;
            }
            //
            SaveAndLoadGameData.instance.savedData.coin = coin;
            SaveAndLoadGameData.instance.Save();
            FindObjectOfType<EntranceUI>().UpdateCoin();
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
