using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalGameManager : MonoBehaviour
{
    public GameObject room;
    public float gameRadius,gameTime,scaleDownStartTime,roomScaleDownChance=0.2f;
    private int score;
    SurvivalGameUI gameUI;
    public bool gameStopped,isGameStarted = false,gameEnded,roomClosing,willRoomScale = false;
    public int waveIndex = 0;
   

    void Start()
    {
        gameUI = FindObjectOfType<SurvivalGameUI>();
        gameTime = 0;
        SetRoom();
    }
    
    void Update()
    {
        if(!gameStopped && isGameStarted)
        {
            gameTime += Time.deltaTime;
            if(roomClosing == false && gameTime > scaleDownStartTime && willRoomScale == true)
            {
                roomClosing = true;
                FindObjectOfType<WallScaler>().CallScaler();
            }
        }
    }
    public void SetRoom()
    {
        gameRadius = Random.Range(30,90);
        room.transform.localScale = new Vector3(gameRadius/3,gameRadius/3,1);
        willRoomScale = ChooseWillRoomScale();
        roomClosing = false;
        FindObjectOfType<CreateRandomWalls>().CreateWalls();
        FindObjectOfType<DeadlyFieldController>().ResetField();
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
    //It works when you touh or click for game to start
    public void StartGame()
    {
        waveIndex++;
        gameEnded = false;
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
        //isGameStarted = false;
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
        //isGameStarted = true;
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
        gameEnded = true;
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
    bool ChooseWillRoomScale()
    {
        float val = Random.Range(0f,1f);
        if(val <= roomScaleDownChance)
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }
    
}
