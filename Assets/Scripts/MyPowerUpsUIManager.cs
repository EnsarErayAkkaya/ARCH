using System.Linq;
using UnityEngine;

public class MyPowerUpsUIManager : MonoBehaviour
{
    [SerializeField] GameObject MyPowerUpCardPrefab,content;
    public void CreateMyActivePowerUps()
    {
        foreach (PowerUpType item in PowerUpManager.powerUpManager.playerPowerUps)
        {
            PowerUp p = PowerUpManager.powerUpManager.powerUps.FirstOrDefault(s => s.powerUpType == item );
            if(p.usageType == UsageType.Temporary)
            {
                UpgradesUI upgrade = Instantiate(MyPowerUpCardPrefab).GetComponent<UpgradesUI>();
                upgrade.powerUpType = item;
                upgrade.powerUpName.text = p.powerUpName;
                upgrade.HiglightButton.image.sprite = p.sprite;
                upgrade.transform.SetParent(content.transform);
            }
        }
    }
}
