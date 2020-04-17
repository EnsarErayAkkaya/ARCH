using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class EntranceUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI coinText,scoreText,totalScoreText;
    void Start()
    {
        UpdateCoin();
        UpdateScore();
        UpdatetotalScore();
    }
    public void UpdateCoin()
    {
        coinText.text = SaveAndLoadGameData.instance.savedData.coin.ToString();
    }
    public void UpdateScore()
    {
        scoreText.color = UnityEngine.Random.ColorHSV(0,1,1,1,1,1);
        scoreText.text = SaveAndLoadGameData.instance.savedData.score.ToString("#,#");
    }
     public void UpdatetotalScore()
    {
        totalScoreText.color = UnityEngine.Random.ColorHSV(0,1,1,1,1,1);
        totalScoreText.text = SaveAndLoadGameData.instance.savedData.totalScore.ToString("#,#");
    }
    public void PlaySurvival()
    {
        MyPowerUpsAutoChooser();
        ProjectileManager.instance.SelectAuto();
        SceneManager.LoadScene(1);
    }
    void MyPowerUpsAutoChooser()
    {
        try
        {
            if(PowerUpManager.powerUpManager.selectedActivePowerUps.Count < 1)
            {
                foreach (PowerUpType item in PowerUpManager.powerUpManager.playerPowerUps)
                {
                    PowerUp p = PowerUpManager.powerUpManager.powerUps
                        .FirstOrDefault(
                            s => 
                                s.powerUpType == item );
                    if(p.usageType == UsageType.Temporary)
                    {
                        PowerUpManager.powerUpManager.SelectPowerUp(p);
                        Debug.Log("a power up automaticly selected");
                        break;
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }        
    }
}
