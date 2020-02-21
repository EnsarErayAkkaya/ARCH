using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerManager : MonoBehaviour
{
    public int minPassangerHealth,maxPassangerHealth;
    public List<Passanger> Passangers;
    public int maxPassangerCapacity;
    public int normalShootDamage,middleshootDamage,powerfulShootDamage;
    public int normalShootEffectCount,middleshootEffectCount,powerfulShootEffectCount;
    StationManager stationManager;
    Player_Shoot player_Shoot;
    
    void Start()
    {
        GetPassangerData();
        player_Shoot = FindObjectOfType<Player_Shoot>();
        stationManager = FindObjectOfType<StationManager>();
    }
    public void GetAllPassangers()
    { 
        while (stationManager.connectedStation.passangersInStation >= 1)
        {
            if( Passangers.Count < maxPassangerCapacity && stationManager.connectedStation.passangersInStation>0 )
            {
                Passangers.Add(new Passanger(Random.Range(minPassangerHealth,maxPassangerHealth+1))); 
                stationManager.connectedStation.passangersInStation--;
                stationManager.connectedStation.station.passangerCount = stationManager.connectedStation.passangersInStation;
                stationManager.CallUpdatePassangerCountText();
            }
            else
            {
                Debug.Log("Yolcu sayısı sınırda. Daha fazla istiyorsanız lütfen kapasitenizi arttırın");
                break;
            }
        }
    }

    //Yolcuların zarar alma fonksiyonu.
    public void GetDamage()
    {
        if(Passangers.Count <= 0)
            return ;
        int damage;
        int effectCount;
        switch (player_Shoot.state)
        {
            case State.NormalShot:
                damage = normalShootDamage;
                effectCount = normalShootEffectCount;
            break;

            case State.MiddleShot:
                damage = middleshootDamage;
                effectCount = middleshootEffectCount;
            break;

            case State.PowerfulShoot:
                damage = powerfulShootDamage;
                effectCount = powerfulShootEffectCount;
            break;
            
            default:
                damage = normalShootDamage;
                effectCount = normalShootEffectCount;
            break;
        }
        for (int i = 0; i < effectCount; i++)
        {
            Passangers[Random.Range(0,Passangers.Count)].GetDamage(damage);
            //Debug.Log("Giving "+ damage + " to passanger");
        }
    }
    public void GetPassanger()
    {
        if( Passangers.Count < maxPassangerCapacity && stationManager.connectedStation.passangersInStation>0 )
        {
            Passangers.Add(new Passanger(Random.Range(minPassangerHealth,maxPassangerHealth+1))); 
            stationManager.connectedStation.passangersInStation--;
            stationManager.connectedStation.station.passangerCount = stationManager.connectedStation.passangersInStation;
            stationManager.CallUpdatePassangerCountText();
        }
        else
        {
            Debug.Log("Yolcu sayısı sınırda. Daha fazla istiyorsanız lütfen kapasitenizi arttırın");
        }
    }
    //Yolcuların durumunu iyileştir.
    public void TreatPassangers()
    {
        //Para eklendiğinde para ile yolcuları tedavi edeceğiz
        //şimdilik beleş
        
    }
    //Yolcu sayısını sıfralar.
    public void LeavePassangers()
    {    

    }
    void GetPassangerData()
    {
        Passangers = SaveAndLoadGameData.instance.savedData.Passangers;
    } 
}
