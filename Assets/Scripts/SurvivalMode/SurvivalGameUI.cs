using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalGameUI : GameUI
{
    public TextMeshProUGUI timeText,scoreText,startText,shrinkText;
    public Button nextButton,restartButton;
    SurvivalGameManager survivalManager;

    void Start()
    {
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
        nextButton.gameObject.SetActive(true);
    }
    public void OnNextButtonClick()
    {
        survivalManager.CleanGame();
        survivalManager.SetRoom();
        survivalManager.isGameStarted = false;
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
        restartButton.gameObject.SetActive(true);
    }
    public void OnClickRestart()
    {
        survivalManager.RestartGame();
    }
}
