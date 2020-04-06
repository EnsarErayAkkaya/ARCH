 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
/*    public List<GameObject> Rooms;
    public GameObject EndingRoom;
    public Material endingRoomMaterial;
    public int dungenLengthX,dungenLengthY;
    public RoomController[,] roomsArray;
    public GameObject room;
    public Vector2 creatorPos = Vector2.zero; 
    public int choosenRoomCount=3,currentRoomCount,X,Y,distanceBetweenRooms;
    public Vector2 randomDirection,lastDirection;
    private GameObject roomsParent;
    private FloorManager floorManager;
    StationManager stationManager;
    void Awake()
    {
        floorManager = FindObjectOfType<FloorManager>();
        stationManager = FindObjectOfType<StationManager>();
        roomsParent = GameObject.Find("Rooms");
        roomsArray = new RoomController[dungenLengthX,dungenLengthY];
    }
    public void ChooseEndingRoom(Floor floor)
    {
        int index = Random.Range(Rooms.Count/2 + Rooms.Count/4,Rooms.Count);
        EndingRoom = Rooms[index];
        RoomController room =EndingRoom.GetComponent<RoomController>();
        room.isEndingRoom = true;
        EndingRoom.GetComponentInChildren<SpriteRenderer>().material = endingRoomMaterial;
        creatorPos = EndingRoom.transform.position;
        
        floor.rooms[index].isEndingRoom = true;

        stationManager.CreateNewStation(room,floor);
        floorManager.floors.Add(floor);
    }

    public void DestroyAllRooms()
    {
        //creatorPos = EndingRoom.transform.position;
        foreach (var item in Rooms)
        {
            Destroy(item);
        }
        Rooms.Clear();
        stationManager.DestroyOldStations();
        roomsArray = new RoomController[dungenLengthX,dungenLengthY];
        currentRoomCount = 0;
    }

    public void CreateDungeon()
    {
        Floor floor = new Floor(floorManager.currentFloor);
        //katı oluşturuyoruz
    
        floor.floorIndex = floorManager.currentFloor;

        X = Random.Range(0,dungenLengthX);
        Y = Random.Range(0,dungenLengthY);
        //Başlangıç değerleri
        floor.startingX = X; floor.startingY = Y;

        //Başlangıç roomunu oluştur
        roomsArray[X,Y] = Instantiate(room, creatorPos,Quaternion.identity).GetComponent<RoomController>();

        Room fRoom = new Room(creatorPos);

        roomsArray[X,Y].room = fRoom;

        floor.rooms.Add(fRoom);
        fRoom.isStartingRoom = true;
        fRoom.roomCleaned = true;
        //istasyon üret
        stationManager.CreateNewStation( roomsArray[X,Y], floor );

        //düşman spawn olmaya && başlangıç odası ola
        roomsArray[X,Y].roomCleaned = true;roomsArray[X,Y].isStartingRoom = true;
        //Parent objesini roomsParent yap
        roomsArray[X,Y].transform.SetParent(roomsParent.transform);
        //Odayı hazırla et (Önceden awake te idi)
        roomsArray[X,Y].Set();

        
        //Yaratılan odaları rooms listesine kaydet
        Rooms.Add(roomsArray[X,Y].gameObject);
        //yaratılan oda sayacını arttır
        currentRoomCount++;

        //tüm odalar yaratılana kadar döngüye gir
        while( currentRoomCount < choosenRoomCount )
        {
            //Rastgele bir yön seç
            ChooseRandomDirection();
            
            fRoom.directions.Add((Dir2)randomDirection);

            if(roomsArray[X + (int)randomDirection.x, Y + (int)randomDirection.y] == null )
            {
                ///burda odayı seçilen yöne göre değiştir kapıları ve köprüleri değiştir
                roomsArray[X,Y].OpenDirection(randomDirection);                
            }
            //index değerlerini rastgele seçilen yöne göre değiştir
            X += (int)randomDirection.x; 
            Y += (int)randomDirection.y;

            //Debug.Log("X,Y : "+ X + ", " + Y);

            //gerçek dünya pozisyonunu yöne göre ata
            creatorPos += distanceBetweenRooms * randomDirection;
            if(roomsArray[X,Y] == null)
            {
                //oda yaratılmamış o zaman...
                
                //odayı yarat ve diziye kaydet
                roomsArray[X,Y] = Instantiate(room, creatorPos,Quaternion.identity).GetComponent<RoomController>();

                fRoom = new Room(creatorPos);

                roomsArray[X,Y].room = fRoom;

                floor.rooms.Add(fRoom);

                //Odayı hazırla et (Önceden awake te idi)
                roomsArray[X,Y].Set();
                //Yaratılan odaları rooms listesine kaydet
                Rooms.Add(roomsArray[X,Y].gameObject);
                //Parent objesini roomsParent yap
                roomsArray[X,Y].transform.SetParent(roomsParent.transform);
                //Yaratılan odanın kapısını aç ilerlenilen yönün tersinde 
                roomsArray[X,Y].OpenDirection(-randomDirection);

                fRoom.directions.Add((Dir2)(-randomDirection));
                //yaratılan oda sayacını arttır
                currentRoomCount++;
            }
            lastDirection = randomDirection;
        }

        ChooseEndingRoom(floor);
    }

    void ChooseRandomDirection()
    {
        //bulunduğumuz konumun komşularını bul
        List<Vector2> neighbours = FindNeighbours(X,Y);
        do
        {
            randomDirection = neighbours[Random.Range(0,neighbours.Count)];
        }
        while (randomDirection == -lastDirection);
       
    }
    //Bir konumun komşularını döndürür
    public List<Vector2> FindNeighbours(int x,int y)
    {
        List<Vector2> list = new List<Vector2>();

        if(x == 0)
            list.Add(new Vector2(1,0));
        else if( x == dungenLengthX-1)
            list.Add(new Vector2(-1,0));
        else{
            list.Add(new Vector2(1,0));
            list.Add(new Vector2(-1,0));
        }

        if(y == 0)
            list.Add(new Vector2(0,1));
        else if( y == dungenLengthY-1)
            list.Add(new Vector2(0,-1));
        else{
            list.Add(new Vector2(0,1));
            list.Add(new Vector2(0,-1));
        }
        return list;
    }

    public void CreateOldDungeon(Floor floor, bool toUp)
    {
        FindObjectOfType<Player_Shoot>().GetComponent<Collider2D>().enabled = false;
        foreach (Room roomData in floor.rooms)
        {
           
            RoomController ro = Instantiate(room, (Vector2)roomData.pos,Quaternion.identity).GetComponent<RoomController>();

            ro.roomCleaned = roomData.roomCleaned;

            //Odayı hazırla et (Önceden awake te idi)
            ro.Set();
            //Yaratılan odaları rooms listesine kaydet
            Rooms.Add(ro.gameObject);
            //Parent objesini roomsParent yap
            ro.transform.SetParent(roomsParent.transform);
            //Yaratılan odanın kapısını aç ilerlenilen yönün tersinde 
            foreach (Vector2 dir in roomData.directions)
            {
                ro.OpenDirection(dir);
            }
             if(roomData.isStartingRoom)
            {
                if(toUp == true)
                {
                    TransferPlayer(roomData.pos);
                }
                
                stationManager.CreateOldStation(ro,floor.stations[0]);
            }
            if(roomData.isEndingRoom)
            {
                if(toUp == false)
                {
                    TransferPlayer(roomData.pos);
                }
                ro.isEndingRoom = true;
                ro.gameObject.GetComponentInChildren<SpriteRenderer>().material = endingRoomMaterial;
                stationManager.CreateOldStation(ro,floor.stations[1]);
            }     
        } 
        FindObjectOfType<Player_Shoot>().GetComponent<Collider2D>().enabled = true;
        foreach (RoomController item in FindObjectsOfType<RoomController>())
        {
            if(item.isEndingRoom) creatorPos = item.transform.position;
        }
    }
    void TransferPlayer(Vector2 pos)
    {
        Player_Shoot pl = FindObjectOfType<Player_Shoot>();
        pl.canRecoil = false;
        pl.transform.position = pos;
        pl.recoiledVector = pos;
    }*/
}
 