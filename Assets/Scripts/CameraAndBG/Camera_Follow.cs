using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;
    public bool isMinimap;
    public float SmoothSpeed = 0.125f,normalBend,middleBend,powerfulBend,staticBend;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(target.position.x,target.position.y,transform.position.z),SmoothSpeed * Time.deltaTime);
            if(!isMinimap)
                CameraBend();
    }
    void CameraBend()
    {
        switch (target.gameObject.GetComponent<Player_Shoot>().state)
        {
            case State.Static:
                Camera.main.orthographicSize = Mathf.Lerp( Camera.main.orthographicSize, staticBend,Time.deltaTime);
               
            break;
            case State.NormalShot:
                Camera.main.orthographicSize = Mathf.Lerp( Camera.main.orthographicSize, normalBend,Time.deltaTime);

            break;
            case State.MiddleShot:
                Camera.main.orthographicSize = Mathf.Lerp( Camera.main.orthographicSize, middleBend,Time.deltaTime);
               
            break;
            case State.PowerfulShoot:
                Camera.main.orthographicSize = Mathf.Lerp( Camera.main.orthographicSize, powerfulBend,Time.deltaTime);
                
            break;
            
        }
    }
}
