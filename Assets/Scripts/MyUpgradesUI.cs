using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyUpgradesUI : MonoBehaviour
{
    public PowerUpType powerUpType;
    public Button HiglightButton;
    [SerializeField]TextMeshProUGUI description,chooseButtonText;
    public TextMeshProUGUI powerUpName;
    bool isHiglighted = false;
    public bool isSelected;
    [SerializeField] string choosedString,notChoosedString;
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
        PowerUp p = PowerUpManager.powerUpManager.powerUps.FirstOrDefault(s => s.powerUpType == this.powerUpType);
        if(!isSelected)
        {
            bool select = PowerUpManager.powerUpManager.SelectPowerUp(p);
            if(select == true)
            {
                isSelected = true;
                chooseButtonText.text = choosedString;
            }
        }
        else
        {
            bool deselect = PowerUpManager.powerUpManager.DeselectPowerUp(p);
            if(deselect == true)
            {
                isSelected = false;
                chooseButtonText.text = notChoosedString;
            }
        }
        
    }
}
