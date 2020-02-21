using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Room room;
    public bool isStartingRoom,isEndingRoom,isPlayerInRoom,roomCleaned;
    public List<GameObject> lockers = new List<GameObject>();
    public List<GameObject> EnemyList;
    float radius = 25;
    public float bridgeScaleX,bridgeScaleY,roomScaleX,roomScaleY;
    private GameObject[] doors;
    public GameObject bridge,floorPortal,longRoom;
    public GameObject[] roomWalls;
    public Transform topBridge,rightBridge;
    public List<GameObject> roomPowerUps;
    public int X,Y;
 
    public void Set()
    {
        RoomWall roomWall; 
     
        roomWall = Instantiate(roomWalls[Random.Range(0,roomWalls.Length)],transform.position,Quaternion.identity).GetComponent<RoomWall>();
        doors = new GameObject[4];
        
        for (int i = 0; i < roomWall.doors.Length; i++)
        {
            doors[i] = roomWall.doors[i];
        }
        roomWall.transform.SetParent(transform);
        roomWall.transform.localScale = new Vector3(roomScaleX,roomScaleY);
        roomPowerUps = new List<GameObject>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(roomCleaned == false && isPlayerInRoom == false)
            {
                other.gameObject.GetComponent<Player>().howManyRoomVisited++;
                isPlayerInRoom = true;
                LockTheRoom();
                SpawnEnemy();
            }
           
        }
    }
    void LockTheRoom()
    {
        StartCoroutine(LockTheRoomRoutine());
    }
    IEnumerator LockTheRoomRoutine()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<Camera_Shake>().Call_Shake(.5f,.25f);///Shakes Camera
        foreach (var item in lockers)
        {
            item.SetActive(true);
        }
    }
    void OpenTheRoom()
    {
        StartCoroutine(OpenTheRoomRoutine());
    }
     IEnumerator OpenTheRoomRoutine()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<Camera_Shake>().Call_Shake(.5f,.13f);///Shakes Camera
        foreach (var item in lockers)
        {
            item.SetActive(false);
        }
        
        //Bir oda geçildiğinde oyunu kaydet diyelim.
        FindObjectOfType<GameManager>().CallSaveGame();
    }
    void SpawnEnemy()
    {
        Enemy_Controller enemy_Controller = FindObjectOfType<Enemy_Controller>();

        int a = enemy_Controller.ChooseEnemyCount();
        for (int i = 0; i < a; i++)
        {
            Vector2 pos = ChooseRandomLocation();
            enemy_Controller.CallSpawnEnemy(pos,this);
        }
    }

    Vector2 ChooseRandomLocation()
    {
        var vector2 = Random.insideUnitCircle * radius;
        return new Vector2(transform.position.x + vector2.x,transform.position.y + vector2.y);
    }
    public void StoryEnemysInList(GameObject enemy)
    {
        EnemyList.Add(enemy);
    }
    public void RemoveEnemyWhenDied(GameObject enemy)
    {
        EnemyList.Remove(enemy);
        CheckIsThereEnemy();
    }
    public void CheckIsThereEnemy()
    {
        if(EnemyList.Count<1)
        {
            roomCleaned = true;
            room.roomCleaned = true;
            if(isEndingRoom== false)
            {
                OpenTheRoom();
            }
            else
            {
                OpenTheRoom();
                //SpawnPowerUps();
            }
        }
    }
    /* void SpawnPowerUps()
    {
        PowerUpType[] list = FindObjectOfType<PowerUpManager>().SelectThreeRandomPowerUp();
        foreach (var item in list)
        {
            FindObjectOfType<PowerUpManager>().Call_SpawnPowerUp(ChooseRandomLocation(),this,item);
        }
    } */
   

    public void OpenDirection(Vector2 dir)
    {
        if( dir == new Vector2(0,1) )//Yön yukarı ise üst kapıyı aç
        {
            doors[0].SetActive(false);
            lockers.Add(doors[0]);
            ///Köprü oluştur
            BuildBridge(topBridge);
        }
        else if( dir == new Vector2(0,-1) )//Aşşağı ise aşşağı kapıyı aç
        {
            doors[1].SetActive(false);
            lockers.Add(doors[1]);
        }
        else if( dir == new Vector2(1,0) )//Sağ is sağ kapıyı aç
        {
            doors[2].SetActive(false);
            lockers.Add(doors[2]);
            ///Köprü oluştur
            BuildBridge(rightBridge);
        }
        else if( dir == new Vector2(-1,0) )//sol ise sol kapıyı aç
        {
            doors[3].SetActive(false);
            lockers.Add(doors[3]);
        }        
    }
    void BuildBridge(Transform bridgePos)
    {
        var ob = Instantiate(bridge,bridgePos.position,bridgePos.rotation);
        ob.transform.SetParent(gameObject.transform);
        ob.transform.localScale = new Vector3(bridgeScaleX,bridgeScaleY,1);
    }

    public void PowerUpChoosen(Vector2 pos)
    {
        if(isEndingRoom)
        {
            Instantiate(floorPortal,pos,Quaternion.identity);
        }
        foreach (var item in roomPowerUps)
        {
            item.GetComponent<PowerUpObject>().DestroyInteractable();
        }
        OpenTheRoom();
    }
}
