using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplateGenerator : MonoBehaviour
{
    private float reflectorWallMinXScale=.35f,reflectorWallMaxXScale=1.4f,reflectorWallMinYScale=.37f,reflectorWallMaxYScale=1.45f;
    public GameObject[] objects;

    void Start()
    {   
        RoomController room = GetComponent<RoomController>();
        if(room != null) 
        {
            int rand = Random.Range(0, objects.Length);
            GameObject template = Instantiate(objects[rand], transform.position, Quaternion.identity);
            template.transform.SetParent(room.transform);
            template.transform.position = new Vector3(transform.position.x, room.transform.position.y+8.25f ,transform.position.z);
        }
        else
        {
            int rand = Random.Range(0, objects.Length);
            GameObject wall =Instantiate(objects[rand], transform.position, Quaternion.identity);
            wall.transform.SetParent(transform);
            wall.transform.localScale = new Vector3(Random.Range(reflectorWallMinXScale,reflectorWallMaxXScale)
                    ,Random.Range(reflectorWallMinYScale,reflectorWallMaxYScale),transform.localScale.z);
        }        
    }

}
