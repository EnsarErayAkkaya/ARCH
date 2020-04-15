using System.Linq;
using UnityEngine;

public class MyPowerUpsUIManager : MonoBehaviour
{
    [SerializeField]Animator upgradesAnimator;
    [SerializeField] GameObject MyPowerUpCardPrefab,content;
    
    public void CreateMyActivePowerUps()
    {
        //Clean first
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        ShowMyUpgrades();
        foreach (PowerUpType item in PowerUpManager.powerUpManager.playerPowerUps)
        {
            PowerUp p = PowerUpManager.powerUpManager.powerUps.FirstOrDefault(s => s.powerUpType == item );
            if(p.usageType == UsageType.Temporary)
            {
                MyUpgradesUI upgrade = Instantiate(MyPowerUpCardPrefab).GetComponent<MyUpgradesUI>();
                
                bool type = PowerUpManager.powerUpManager.selectedActivePowerUps.Any( s => s == item);
                if(type == true)
                {
                    //true döndüğüne göre seçilmiş demektir.
                    upgrade.isSelected = true;
                    upgrade.chooseButtonText.text = upgrade.choosedString;
                }
                else
                {
                    //false döndüyse seçilmemiş demektir.
                    upgrade.isSelected = false;
                    upgrade.chooseButtonText.text = upgrade.notChoosedString;
                }
                
                upgrade.powerUpType = item;
                upgrade.powerUpName.text = p.powerUpName;
                upgrade.HiglightButton.image.sprite = p.sprite;
                upgrade.transform.SetParent(content.transform);
                upgrade.description.text = p.description;
            }
        }
    }
    public void ShowMyUpgrades()
    {
        upgradesAnimator.Play("ShowMyPowerUps");
    }
    public void ShowAllUpgrades()
    {
        upgradesAnimator.Play("ShowAllPowerUps");
    }
}
