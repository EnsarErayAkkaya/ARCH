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
                upgrade.powerUpType = item;
                upgrade.powerUpName.text = p.powerUpName;
                upgrade.HiglightButton.image.sprite = p.sprite;
                upgrade.transform.SetParent(content.transform);
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
