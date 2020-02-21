using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalGameManager : MonoBehaviour
{
    public GameObject room;
    public float gameRadius;
    private int score;
    public float gameTime;
    SurvivalGameUI gameUI;
    public bool gameStopped,isGameStarted = false;
    public int waveIndex = 0;
    void Start()
    {
        gameUI = FindObjectOfType<SurvivalGameUI>();
        gameTime = 0;
        SetRoom();
    }
    public void SetRoom()
    {
        gameRadius = Random.Range(30,90);
        room.transform.localScale = new Vector3(gameRadius/3,gameRadius/3,1);
        FindObjectOfType<CreateRandomWalls>().CreateWalls();
    }
    public void GetScore()
    {
        score += 100;
        gameUI.UpdateScoreText(score);
    }
    public void GetEnemyScore()
    {
        score += 50;
        gameUI.UpdateScoreText(score);
    }
    public void LoseScore()
    {
        score -= 100;
        gameUI.UpdateScoreText(score);
    }
    
    public void StartOrGoNextGame()
    {
        waveIndex++;
        isGameStarted = true;
        gameStopped = false;
        FindObjectOfType<Player_Shoot>().enabled = true;
        //Oluştur
        FindObjectOfType<CheckPointManager>().CreateCheckPoints();
        FindObjectOfType<SurvivalEnemyManager>().ProduceEnemys();
    }
    public void StopGame()
    {
        gameStopped = true;
        //düşmanları sakla
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            item.gameObject.SetActive(false);   
        }
        foreach (var item in FindObjectsOfType<AddForceToWall>())
        {
            item.gameObject.SetActive(false);   
        }
        FindObjectOfType<Player_Shoot>().enabled = false;
    }
    public void ResumeGame()
    {
        gameStopped = false;
        //düşmanları açığa çıkar
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            item.gameObject.SetActive(true); 
        }
        foreach (var item in FindObjectsOfType<AddForceToWall>())
        {
            item.gameObject.SetActive(true);   
        }
        FindObjectOfType<Player_Shoot>().enabled = true;
    }
    public void CleanGame()
    {
        gameTime = 0;
        FindObjectOfType<Player_Shoot>().transform.position = Vector2.zero;
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in FindObjectsOfType<CheckpointController>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in FindObjectsOfType<AddForceToWall>())
        {
            Destroy(item.gameObject);
        }
    }
    public void EndWave()
    {
        //Son skoru hesapla
        CalculateTimeScore();
        gameUI.UpdateScoreText(score);
    }
    
    private void CalculateTimeScore()
    {
        if( gameTime <= 25 )
        {
            score += 500;
        }
        else if( gameTime > 25 && gameTime <= 50 )
        {
            score += 300;
        }
        else if( gameTime > 50 && gameTime <=75 )
        {
            score += 100;
        }
    }
    public void EndGame()
    {
        //Score u kaydet ve her şeyi sıfırla
    }
    
}
