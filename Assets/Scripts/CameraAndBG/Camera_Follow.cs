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
                 GetComponent<Camera>().orthographicSize = Mathf.Lerp( GetComponent<Camera>().orthographicSize, staticBend,Time.deltaTime);
               
            break;
            case State.NormalShot:
                GetComponent<Camera>().orthographicSize = Mathf.Lerp( GetComponent<Camera>().orthographicSize, normalBend,Time.deltaTime);

            break;
            case State.MiddleShot:
                GetComponent<Camera>().orthographicSize = Mathf.Lerp( GetComponent<Camera>().orthographicSize, middleBend,Time.deltaTime);
               
            break;
            case State.PowerfulShoot:
                GetComponent<Camera>().orthographicSize = Mathf.Lerp( GetComponent<Camera>().orthographicSize, powerfulBend,Time.deltaTime);
                
            break;
            
        }
    }
}
