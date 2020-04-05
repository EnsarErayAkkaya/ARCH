using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    public PowerUpType powerUpType;
    public Button HiglightButton;
    [SerializeField]int price;
    [SerializeField]TextMeshProUGUI description,priceText;
    public TextMeshProUGUI powerUpName;
    bool isHiglighted = false;
    public bool isMyPowerUp;
    void Start()
    {
        if(!isMyPowerUp)
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

    public void onChooseButtonClick()
    {
        
    }
}
