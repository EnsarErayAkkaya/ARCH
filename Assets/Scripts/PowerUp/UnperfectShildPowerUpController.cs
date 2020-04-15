using System.Linq;
using UnityEngine;

public class UnperfectShildPowerUpController : MonoBehaviour
{
    PowerUpType type = PowerUpType.UnPerfectShield;
    
    void Start()
    {
        PowerUp p = PowerUpManager.powerUpManager.powerUps.FirstOrDefault(s => s.powerUpType == type);
        Destroy(gameObject, p.usingTime);
    }
}
