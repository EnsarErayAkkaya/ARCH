using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
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
        SceneManager.LoadScene(1);
    }
}
