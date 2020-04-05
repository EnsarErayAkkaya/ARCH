using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager powerUpManager;
    ///All power ups in game
    public List<PowerUp> powerUps;
    ///All power ups player has
    public List<PowerUpType> playerPowerUps;
    ///five power up player selected 3 active 2 passive
    public List<PowerUp> selectedActivePowerUps;
    [SerializeField] int activePowerUplimit;
    void Awake()
    {
        if (PowerUpManager.powerUpManager == null)
        {
            PowerUpManager.powerUpManager = this;
        }
        else if (PowerUpManager.powerUpManager != null)
        {
            Destroy(PowerUpManager.powerUpManager.gameObject);
            PowerUpManager.powerUpManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
        playerPowerUps = SaveAndLoadGameData.instance.savedData.playerPowerUps;
    }


    ///Just For temporary powers
    ////Oyuncu yeteneği aktif hale getirince çağıralacak
    public void GivePower(GameObject player, PowerUpType powerUpType)
    {
        Player_Shoot pShoot = player.GetComponent<Player_Shoot>();
        Player p = player.GetComponent<Player>();
        PowerUp powerUp = powerUps.FirstOrDefault( s => s.powerUpType == powerUpType);
        
        ///Eğer aktif bir power up varsa geri dön
        ///Bunu şimdilik geçici bir çözüm olarak koyuyorum belki power uplar stacklenebilir bilmiyorum.
        if(p.isThereActivePowerUp)
        {
            return;
        }
        ///Geçici kod buraya kadar

        switch (powerUpType)
        {
            case PowerUpType.MachineGun:
                Debug.Log("machine gun 0");
                pShoot.canRecoil = false;
                powerUp.tempData.Add(pShoot.NormalShootTimeLimit);
                pShoot.NormalShootTimeLimit = 0.2f;
                p.isThereActivePowerUp = true;
                StartCoroutine( GetPowerBack(player, powerUpType) );
            break;

            default:
            break;
        }
    }
    public IEnumerator GetPowerBack(GameObject player, PowerUpType powerUpType)
    {
        Player_Shoot pShoot = player.GetComponent<Player_Shoot>();
        Player p = player.GetComponent<Player>();
        if(PowerUpType.MachineGun == powerUpType)
        {
            PowerUp powerUp = powerUps.FirstOrDefault( s => s.powerUpType == powerUpType);
            if(powerUp.usageType == UsageType.Temporary)
            {
                Debug.Log("machine gun 1");
                yield return new WaitForSeconds(powerUp.usingTime);
                pShoot.NormalShootTimeLimit =powerUp.tempData[0];
                pShoot.canRecoil = true;
                p.isThereActivePowerUp = false;
            }
        }
    }
    public void ObtainPower(PowerUpType powerUpType)
    {
        playerPowerUps.Add(powerUpType);
        //SelectPowerUp(powerUp);
    }
    public void ObtainPower(PowerUp powerUp)
    {
        playerPowerUps.Add(powerUp.powerUpType);
        //SelectPowerUp(powerUp);
    }
    void SavePlayersPowerUp()
    {
        SaveAndLoadGameData.instance.savedData.playerPowerUps = this.playerPowerUps;
        SaveAndLoadGameData.instance.Save();
    }
    public void SelectPowerUp(PowerUp powerUp)
    {
       
        if(powerUp.usageType == UsageType.Temporary && selectedActivePowerUps.Count < activePowerUplimit)
        {
            selectedActivePowerUps.Add(powerUp);
            Debug.Log("Power up selected. "+ powerUp.powerUpName);
        }
        else
        {
            Debug.Log("You can not have more then"+ activePowerUplimit +"power up 3 Active");
        }
    }
    public void DeselectPowerUp(PowerUp powerUp)
    {
        if(selectedActivePowerUps.Count > 0)
        {
            if(powerUp.usageType == UsageType.Temporary && selectedActivePowerUps.Count > 0)
            {
                selectedActivePowerUps.Remove(powerUp);
            }
        }
        else
        {
            Debug.Log("There is no power up selected. Select one for remove");
        }
    }
    /* public PowerUpType[] SelectThreeRandomPowerUp()
    {
        List<PowerUp> list = new List<PowerUp>();
        list.AddRange(powerUps);
        PowerUpType[] powers = new PowerUpType[3];
        
        for (int i = 0; i < 3; i++)
        {
            int a = UnityEngine.Random.Range(0,list.Count);
            powers[i] = list[a].powerUpType;
            list.RemoveAt(a);
        }
        return powers;
    } */

   /*  public void Call_SpawnPowerUp(Vector2 pos,RoomController room,PowerUpType powerUpType)
    {
        StartCoroutine(SpawnPowerUpEnumerator(pos,room, onPowerUpCreated,powerUpType));
    }
     public IEnumerator SpawnPowerUpEnumerator(Vector2 pos,
        RoomController room,
        Action<PowerUpObject,RoomController,PowerUpType> onPowerUpCreated,
        PowerUpType powerUpType)
    {

        var particle = Instantiate(powerUpSpawnParticle,pos,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
    
        yield return new WaitForSeconds(2f);
            
        PowerUpObject enemy = SpawnPowerUp(pos);
        onPowerUpCreated(enemy,room,powerUpType);
    }
    private PowerUpObject SpawnPowerUp(Vector2 pos)
    {
        return Instantiate(powerUpObject,pos,Quaternion.identity).GetComponent<PowerUpObject>();
    }
    void onPowerUpCreated(PowerUpObject obj,RoomController room,PowerUpType powerUpType)
    {
        obj.powerUpType = powerUpType;
        obj.SetUp();
        obj.room = room;
        room.roomPowerUps.Add(obj.gameObject);
    } */
}
