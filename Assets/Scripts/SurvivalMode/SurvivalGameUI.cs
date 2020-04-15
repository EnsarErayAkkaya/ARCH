using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalGameUI : GameUI
{
    [SerializeField]GameObject gameEndedGroup,pausedGroup;
    public TextMeshProUGUI timeText,scoreText,startText,shrinkText,coinGainedText,totalCoinText;
    public Button nextButton,restartButton,pauseButton;
    SurvivalGameManager survivalManager;
    ActivePowerUpGameUI activePowerUp;
    public bool canShowAd = false;
    bool stopCoroutines = false;
    void Start()
    {
        activePowerUp = FindObjectOfType<ActivePowerUpGameUI>();
        survivalManager = FindObjectOfType<SurvivalGameManager>();
    }   
    void Update()
    {
        if(survivalManager.isGameStarted == false)
        {
            startText.gameObject.SetActive(true);
            if(Input.GetMouseButton(0))
            {
                survivalManager.StartGame();
                startText.gameObject.SetActive(false);
                pauseButton.gameObject.SetActive(true);
                activePowerUp.useButton.enabled = true;
                if(survivalManager.willRoomScale)
                    shrinkText.gameObject.SetActive(true);
            }
        }
        if(!survivalManager.gameStopped && survivalManager.isGameStarted)
        { 
            timeText.text = survivalManager.gameTime.ToString("#.#");   
        }
    }
    public void UpdateScoreText(int score)
    {
        //scoreText.text = score.ToString();
        scoreText.color = UnityEngine.Random.ColorHSV(0,1,1,1,1,1);
        StartCoroutine( UpdateScoreEnumerator(score) );
    }
    public void SetUIOnGamePassed()
    {
        activePowerUp.useButton.enabled = false;
        nextButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
    public void OnNextButtonClick()
    {
        survivalManager.CleanGame();
        survivalManager.SetRoom();
        nextButton.gameObject.SetActive(false);
    }
    IEnumerator UpdateScoreEnumerator(int score)
    {
        int cScore = Convert.ToInt32(scoreText.text);
        while (cScore < score) {
            cScore += 2;
            scoreText.text = cScore.ToString();
            yield return null;
        }
    }
    public void EndGameUI()
    {
        if(canShowAd && SaveAndLoadGameData.instance.savedData.playedGameCount%2 == 0)
        {
            FindObjectOfType<RegularAdScript>().ShowRegularAd();
        }
        pauseButton.gameObject.SetActive(false);
        gameEndedGroup.SetActive(true);
        totalCoinText.text = (SaveAndLoadGameData.instance.savedData.coin - survivalManager.GetCoinGained()).ToString();
        CallCoinEnumerator();
    }
    public void HideEndGameUI()
    {
        gameEndedGroup.SetActive(false);
    }
    public void CallCoinEnumerator()
    {
        StartCoroutine( UpdateCoinGainedEnumerator( survivalManager.GetCoinGained() ) );
    }
    IEnumerator UpdateCoinGainedEnumerator(int coin)
    {
        int i = 0;
        while (i < coin) {
            i += 5;
            coinGainedText.text = i.ToString();
            yield return null;
        }
        StartCoroutine( UpdateTotalCoinEnumerator(coin) );
    }
    IEnumerator UpdateTotalCoinEnumerator(int coin)
    {
        int oldCoin = Convert.ToInt32( totalCoinText.text);
        coin += oldCoin;
        while (oldCoin < coin) {
            oldCoin += 5;
            totalCoinText.text = oldCoin.ToString();
            yield return null;
        }
    }
    public void OnClickRestart()
    {
        survivalManager.RestartGame();
    }
    public void OnClickReturnHome()
    {
        survivalManager.ReturnHome();
    }
    public void PauseGame()
    {
        if(survivalManager.gameStopped == true)
        {
            //Oyunu devam ettir
            survivalManager.ResumeGame();
            activePowerUp.useButton.enabled = true;
            pausedGroup.SetActive(false);
        }
        else{
            //oyunu duraklat
            survivalManager.StopGame();
            activePowerUp.useButton.enabled = false;
            pausedGroup.SetActive(true);
        }
    }
    public void EndGame()
    {
        pausedGroup.SetActive(false);
        survivalManager.EndGame();
    }
}
